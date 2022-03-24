using GiftGivingGenerator.API;
using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.Repositories;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
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
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IDrawingResultRepository, DrawingResultRepository>();
builder.Services.AddScoped<IGiftWishRepository, GiftWishRepository>();
builder.Services.AddScoped<IMailService, MailService>();

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

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		corsPolicyBuilder =>
		{
			corsPolicyBuilder.WithOrigins(builder.Configuration.GetValue<string[]>("AllowedHosts"));
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

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();