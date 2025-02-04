
using AdvicerApp.BL;
using AdvicerApp.BL.Extensions;
using AdvicerApp.Core.Entities;
using AdvicerApp.DAL;
using AdvicerApp.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AdvicerApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddDbContext<AdvicerAppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Dell"));
        });
        builder.Services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequiredLength = 5;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireUppercase = false;
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AdvicerAppDbContext>();
        builder.Services.AddServices();
        builder.Services.AddFluentValidation();
        builder.Services.AddAutoMapper();
        builder.Services.AddRepositories();
        builder.Services.AddAuth(builder.Configuration);
        builder.Services.AddJwtOptions(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.EnablePersistAuthorization();
            });
        }
        app.ExceptionHandler();
        app.UseHttpsRedirection();
        app.UseSeedData();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
