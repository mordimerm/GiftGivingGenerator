using GiftGivingGenerator.API;
using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.HashingPassword;
using GiftGivingGenerator.API.Repositories;
using GiftGivingGenerator.API.Repositories.Abstractions;
using GiftGivingGenerator.API.Servicess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IDrawingResultRepository, DrawingResultRepository>();
builder.Services.AddScoped<IGiftWishRepository, GiftWishRepository>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("MailAccess"));

builder.Services.AddLogging(x => x.AddSerilog());
builder.Services.AddSingleton<HashingOptions>();
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	.MinimumLevel.Override("System", LogEventLevel.Error)
	.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();
Log.Information("****************************** Started ******************************");

builder.Services.AddScoped<ISeeder, Seeder>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();