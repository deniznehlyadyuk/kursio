using HealthChecks.UI.Client;
using Kursio.Api.Extensions;
using Kursio.Api.Middleware;
using Kursio.Api.OpenTelemetry;
using Kursio.Common.Application;
using Kursio.Common.Infrastructure;
using Kursio.Common.Infrastructure.EventBus;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Infrastructure;
using Kursio.Modules.Teachers.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication([
    Kursio.Modules.Students.Application.AssemblyReference.Assembly,
    Kursio.Modules.Teachers.Application.AssemblyReference.Assembly,
]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionString("Queue")!);

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [TeachersModule.ConfigureConsumers],
    rabbitMqSettings,
    databaseConnectionString,
    redisConnectionString);

builder.Configuration.AddModuleConfiguration(["students", "teachers"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRabbitMQ(rabbitConnectionString: rabbitMqSettings.Host)
    .AddRedis(redisConnectionString);

builder.Services.AddStudentsModule(builder.Configuration);
builder.Services.AddTeachersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapEndpoints();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

await app.RunAsync();
