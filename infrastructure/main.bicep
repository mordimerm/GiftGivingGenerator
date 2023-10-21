param name string = 'giftgivinggenerator'
param location string = resourceGroup().location
param repositoryUrl string
param branch string
param environment string
param frontendUrl string

param sqlAdminLogin string

@secure()
param sqlAdminPassword string

var tags = {
  environment: environment
}
var locationSuffix = substring(location, 0, 6)

resource sqlServer 'Microsoft.Sql/servers@2023-02-01-preview' = {
  name: 'sql-${name}-${environment}-${locationSuffix}'
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
  }
  
  resource firewallRules 'firewallRules@2023-02-01-preview' = {
    name: 'AllowAllWindowsAzureIps'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }
}

resource sqlDb 'Microsoft.Sql/servers/databases@2023-02-01-preview' = {
  name: name
  parent: sqlServer
  location: location
  sku: {
    name: 'GP_S_Gen5_2'
    tier: 'GeneralPurpose'
  }
  properties: {
    collation: 'Latin1_General_100_CI_AI_SC_UTF8'
    zoneRedundant: false
    autoPauseDelay: 60
    useFreeLimit: true
    freeLimitExhaustionBehavior: 'AutoPause'
  }
}

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: 'log-${name}-${environment}-${locationSuffix}'
  location: location
  tags: tags
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'appi-${name}-${environment}-${locationSuffix}'
  location: location
  tags: tags
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
    Flow_Type: 'Bluefield'
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: 'asp-${name}-${environment}-${locationSuffix}'
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: 'F1'
  }
  kind: 'linux'
  tags: tags
}

resource api 'Microsoft.Web/sites@2022-09-01' = {
  name: 'app-${name}-${environment}-${locationSuffix}'
  location: location
  tags: tags
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|7.0'
    }
    httpsOnly: true
  }

  resource srcControls 'sourcecontrols@2022-09-01' = {
    name: 'web'
    properties: {
      repoUrl: repositoryUrl
      branch: branch
      isManualIntegration: true
    }
  }

  resource config 'config@2022-09-01' = {
    name: 'web'
    properties: {
      connectionStrings: [
        {
          name: 'Db'
          connectionString: 'server=tcp:${sqlServer.name}.database.windows.net,1433;Database=${sqlDb.name};User ID=${sqlAdminLogin};Password=${sqlAdminPassword};Trusted_Connection=False;Encrypt=True;'
        }
      ]
      appSettings: [
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: applicationInsights.properties.ConnectionString
        }
        {
          name: 'WebApplicationUrl'
          value: frontendUrl
        }
      ]
      cors: {
        allowedOrigins: [
          'https://arinstreal.github.io'
        ]
      }
    }
  }
}
