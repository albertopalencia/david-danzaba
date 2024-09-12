using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using RealState.Api.ApiHandlers.Holidays;
using RealState.Api.Filters;
using RealState.Api.Middleware;
using RealState.Infrastructure.DataSource;
using RealState.Infrastructure.Extensions;
using Serilog;
using Serilog.Debugging;
using System.Reflection;
using RealState.Api.ApiHandlers.Properties;
using RealState.Api.ApiHandlers.PropertyImages;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(config.GetConnectionString("db"));
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<DataContext>()
    .ForwardToPrometheus();

builder.Services.AddAutoMapper(Assembly.Load("RealState.Application"));

builder.Services.AddServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.Load("RealState.Application"), typeof(Program).Assembly);

builder.Host.UseSerilog((_, loggerconfiguration) =>
    loggerconfiguration
        .WriteTo.Console()
        .WriteTo.File("logs.txt", Serilog.Events.LogEventLevel.Information));

SelfLog.Enable(Console.Error);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpMetrics();

app.UseMiddleware<AppExceptionHandlerMiddleware>();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseRouting().UseEndpoints(endpoint =>
{
    endpoint.MapMetrics();
});

app.MapGroup("/api/property")
    .MapProperty()
    .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
    .WithTags("Properties");  


app.MapGroup("/api/propertyImage")
    .MapPropertyImage()
    .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
    .WithTags("Property Images");

app.MapGroup("/api/owner")
    .MapOwner()
    .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
    .WithTags("Owners");


app.Run();
