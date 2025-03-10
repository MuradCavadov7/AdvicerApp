using AdvicerApp.BL.ExternalServices.Implements;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Implements;
using AdvicerApp.BL.Services.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvicerApp.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IOwnerApproveService, OwnerApproveService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IStatusCommentService, StatusCommentService>();
        services.AddScoped<IJwtHandler, JwtHandler>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IEmailSend, EmailSend>();
        services.AddMemoryCache();
        return services;
    }
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration =
                configuration.GetConnectionString("Redis");
                opt.InstanceName = "Advicer App_";
            });
            services.AddScoped<ICacheService, RedisService>();
        return services;
    }
    public static IServiceCollection AddHttpContextAcs(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        return services;
    }
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));
        return services;
    }
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}
