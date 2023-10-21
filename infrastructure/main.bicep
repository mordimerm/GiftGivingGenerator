param name string = 'giftgivinggenerator'
param location string = resourceGroup().location
param repositoryUrl string
param branch string
param environment string

var tags = {
  environment: environment
}
var locationSuffix = substring(location, 0, 6)

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

resource databaseAccount 'Microsoft.DocumentDB/databaseAccounts@2023-09-15' = {
  name: 'cosmos-${name}-${environment}-${locationSuffix}'
  location: location
  tags: tags
  properties: {
    enableFreeTier: true
    databaseAccountOfferType: 'Standard'
    consistencyPolicy: {
      defaultConsistencyLevel: 'Session'
    }
    locations: [
      {
        locationName: location
      }
    ]
  }

  resource sqlDatabase 'sqlDatabases@2023-09-15' = {
    name: name
    properties: {
      resource: {
        id: name
      }
      options: {
        throughput: 400
      }
    }
  }
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
          connectionString: databaseAccount.listConnectionStrings().connectionStrings[0].connectionString
        }
      ]
    }
  }
}
