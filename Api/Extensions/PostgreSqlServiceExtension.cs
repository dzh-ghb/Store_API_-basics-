using Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extension
{
    public static class PostgreSqlServiceExtension
    {
        public static void AddPostgreSqlDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<AppDbContext>(options =>
             {
                 options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
             });
        }

        // преднастройка для работы с пользователями
        public static void AddPostgreSqlIdentityContext(this IServiceCollection services)
        {
            // тип пользователей, тип ролей
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}