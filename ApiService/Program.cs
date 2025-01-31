using CacheManager.src.BackgroundWorker;
using CacheManager.src.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.File(configuration["Logging:File:Path"], rollingInterval: RollingInterval.Day)
            .CreateLogger();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding CacheManager Background Service
builder.Services.AddHostedService<DpapiBackgroundService>();

//Adding CacheManager Service
builder.Services.AddSingleton<IDpapiCacheManagerSvc, DpapiCacheManagerSvc>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("################# LOGGING STARTED ###################");


app.Run();
