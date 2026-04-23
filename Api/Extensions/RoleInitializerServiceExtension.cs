using Api.Common;
using Microsoft.AspNetCore.Identity;

// расширение для наполнения таблицы с ролями
namespace Api.Extension
{
    public static class RoleInitializerServiceExtension
    {
        public static async Task InitializeRoleAsync(
            this IServiceProvider serviceProvider
        )
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in SharedData.Roles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}