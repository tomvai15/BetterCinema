using BetterCinema.Api.Constants;
using BetterCinema.Api.Providers;
using BetterCinema.Api.Cryptography;
using BetterCinema.Api.Validation.Handlers;
using BetterCinema.Api.Validation.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BetterCinema.Api.Bootstrap
{
    public static class AuthorizationBootstrap
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy(Policy.DevelopmentCors, builder =>
                {
                    builder.WithOrigins("https://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed((x) => true)
                           .AllowCredentials();
                });
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(configuration.GetSection("Security:Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicy.Owner, policy =>
                    policy.RequireRole(Role.Owner));

                options.AddPolicy(AuthPolicy.TheaterIdInRouteValidation, policy =>
                    policy.Requirements.Add(new TheaterIdInRouteRequirement()));
            });



            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IHasherAdapter, HasherAdapter>();
            services.AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>();
            services.AddTransient<IClaimsProvider, ClaimsProvider>();
            services.AddTransient<IAuthorizationHandler, TheaterIdInRouteValidationHandler>();

            return services;
        }
    }
}
