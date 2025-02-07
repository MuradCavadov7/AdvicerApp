using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdvicerApp.DAL;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<IMenuRepository,MenuRepository>();
        services.AddScoped<IRestaurantRepository,RestaurantRepository>();
        services.AddScoped<ICommentRepository,CommentRepository>();
        services.AddScoped<IRatingRepository,RatingRepository>();   

        return services;
    }

}
