using InvestmentAuthentication.Repositories;
using InvestmentAuthentication.Repositories.Interfaces;
using InvestmentAuthentication.Services;
using InvestmentAuthentication.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace InvestmentAuthentication.Helpers.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Investment Authentication API",
                    Version = "v1"
                });
            });

            return services;
        }

        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

