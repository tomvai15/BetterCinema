using BetterCinema.Api;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IServiceCollection services = builder.Services;
IConfiguration config = builder.Configuration;

services.AddApiServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.AddInitialData();
}

app.UseCors(Policy.DevelopmentCors);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }