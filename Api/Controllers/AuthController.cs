using System.Net;
using Api.Common;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto == null)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Невалидная модель данных" }
                });
            }

            AppUser userFromDb = await Task.FromResult(storage.GetUser(registerRequestDto));
            // dbContext.AppUsers
            // .FirstOrDefaultAsync(u => u.UserName.ToLower() == registerRequestDto.UserName.ToLower());

            if (userFromDb != null)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Пользователь с такими данными уже существует" }
                });
            }

            // AppUser newAppUser = new AppUser
            // {
            //     UserName = registerRequestDto.UserName,
            //     Email = registerRequestDto.Email,
            //     // NormalizedEmail = registerRequestDto.Email.ToUpper(),
            //     FirstName = registerRequestDto.UserName
            // };

            var isUserAdded = await Task.FromResult(storage.AddUser(registerRequestDto, userManager));
            // // попытка создания юзера
            // var result = await userManager.CreateAsync(newAppUser, registerRequestDto.Password);

            // if (!result.Succeeded)
            if (await isUserAdded == false)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Ошибка регистрации" }
                });
            }

            // // определение указанной роли
            // var newRoleAppUser = registerRequestDto.Role.Equals(
            //     SharedData.Roles.Admin, StringComparison.OrdinalIgnoreCase)
            //     ? SharedData.Roles.Admin
            //     : SharedData.Roles.Consumer;

            // // попытка добавления юзера к роли
            // await userManager.AddToRoleAsync(newAppUser, newRoleAppUser);

            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = "Регистрация успешно завершена"
            });
        }

        #region test
        // [HttpGet("/test")]
        // public async Task<ActionResult<ServerResponse>> GetTest()
        // {
        //     return new ServerResponse
        //     {
        //         StatusCode = HttpStatusCode.OK,
        //         Result = await Task.FromResult("test")
        //     };
        // }
        #endregion
    }
}