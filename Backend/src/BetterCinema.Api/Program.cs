using BetterCinema.Api;
using BetterCinema.Api.Constants;
using BetterCinema.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
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

public partial class Program { }