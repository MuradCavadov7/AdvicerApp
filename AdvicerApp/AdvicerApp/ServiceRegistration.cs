﻿using AdvicerApp.BL.DTOs.Options;
using AdvicerApp.BL.Exceptions;
using AdvicerApp.BL.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AdvicerApp;

public static class ServiceRegistration
{
    public static IApplicationBuilder ExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = feature!.Error;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                if (exception is IBaseException ibe)
                {
                    context.Response.StatusCode = ibe.StatusCode;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ibe.StatusCode,
                        Message = ibe.ErrorMessage
                    });
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = exception.Message
                    });
                }

            });

        });
        return app;
    }
    public static IServiceCollection AddSmtpOptions(this IServiceCollection services,IConfiguration configuration)
    {
        var opt = new SmtpOptions();
        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.Name));
        return services;
    }
    public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.Jwt));
        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOpt = configuration.GetSection("JwtOptions").Get<JwtOptions>()!;
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.SecretKey));
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtOpt.Issuer,
                ValidAudience = jwtOpt.Audience,
                IssuerSigningKey = signInKey,
                ClockSkew = TimeSpan.Zero,

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });
        return services;

    }
}
