using Api.Model;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers
{
    public class AuthController : StoreController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(
            IStorage storage
            , UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager)
            : base(storage)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

    }
}