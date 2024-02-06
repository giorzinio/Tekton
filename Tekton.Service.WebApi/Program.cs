using Tekton.Persistence;
using Tekton.Application.UseCases;
using Asp.Versioning.ApiExplorer;
using Tekton.Service.WebApi.Modules.Swagger;
using Tekton.Service.WebApi.Modules.Versioning;
using HealthChecks.UI.Client;
using Tekton.Service.WebApi.Modules.HealthCheck;
using Tekton.Service.WebApi.Modules.RateLimiter;
using Tekton.Service.WebApi.Modules.Middleware;
using Tekton.Service.WebApi.Modules.GlobalException;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddVersioning();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRatelimiting(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
    app.UseReDoc(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.DocumentTitle = "Prueba Tekton Services API";
            options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware();

app.MapHealthChecksUI();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
