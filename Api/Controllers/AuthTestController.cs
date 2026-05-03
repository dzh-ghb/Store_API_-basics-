using System.Net;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthTestController : StoreController
    {
        public AuthTestController(IStorage storage) : base(storage)
        {
        }

        [HttpGet]
        public IActionResult Test1()
        {
            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = "Test1: для всех юзеров"
            });
        }

        [HttpGet]
        [Authorize] // доступен только авторизованным юзерам (с токеном в заголовке запроса)
        public IActionResult Test2()
        {
            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = "Test2: для авторизованных юзеров"
            });
        }

        [HttpGet]
        [Authorize(Roles = SharedData.Roles.Consumer)]
        public IActionResult Test3()
        {
            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = "Test3: для авторизованных юзеров с правами Consumer"
            });
        }

        [HttpGet]
        [Authorize(Roles = SharedData.Roles.Admin)]
        public IActionResult Test4()
        {
            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = "Test4: для авторизованных юзеров с правами Admin"
            });
        }
    }
}