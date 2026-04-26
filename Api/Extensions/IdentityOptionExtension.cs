using Microsoft.AspNetCore.Identity;

// расширения для конфигурации требований к сложности паролей пользователей
namespace Api.Extension
{
    public static class IdentityOptionExtension
    {
        public static IServiceCollection AddConfigureIdentityOptions(
            this IServiceCollection services
        )
        {
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            });

            return services;
        }
    }
}