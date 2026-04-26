using System.Net;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            if (userFromDb != null)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Пользователь с такими данными уже существует" }
                });
            }

            var isUserAdded = await Task.FromResult(storage.AddUser(registerRequestDto, userManager));

            if (await isUserAdded == false)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Ошибка регистрации" }
                });
            }

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