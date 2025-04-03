using BetterCinema.Api;
using BetterCinema.Api.Constants;
using BetterCinema.Infrastructure.Data;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    var services = builder.Services;
    services.AddSerilog();
    builder.Services.AddHttpLogging(o => { });
    IConfiguration config = builder.Configuration;

    services.AddApiServices(config);
    builder.Services.AddHealthChecks();

    var app = builder.Build();

    app.UseHttpLogging();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Services.AddInitialData();
    }

    app.UseCors(Policy.DevelopmentCors);
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{
}