using GiftGivingGenerator.API;
using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.Repositories;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using AppContext = GiftGivingGenerator.API.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<AppContext>(x =>
	x.UseSqlServer(builder.Configuration.GetConnectionString("Db"))
);
builder.Services.AddHealthChecks()
	.AddSqlServer(builder.Configuration.GetConnectionString("Db")!);
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IDrawingResultRepository, DrawingResultRepository>();
builder.Services.AddScoped<IGiftWishRepository, GiftWishRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("MailAccess"));
builder.Services.Configure<AllowedHostsConfiguration>(builder.Configuration.GetSection("AllowedHosts"));
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddLogging(x => x.AddSerilog());
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	.MinimumLevel.Override("System", LogEventLevel.Error)
	.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();
Log.Information("****************************** Started ******************************");

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
		corsPolicyBuilder =>
		{
			corsPolicyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedHosts").Get<string[]>())
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

var app = builder.Build();

app.UseExceptionHandler("/Error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();