using System.Net;
using Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : StoreController
    {
        public ProductsController(IStorage storage) : base(storage)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            ResponseServer response = new ResponseServer
            {
                StatusCode = HttpStatusCode.OK,
                Result = await Task.FromResult(storage.GetAllProducts())
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ResponseServer
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Указан некорректный ID" }
                });
            }

            Product result = await Task.FromResult(storage.GetProduct(id));

            if (result == null)
            {
                return NotFound(new ResponseServer
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Продукт по указанному ID не найден" }
                });
            }
            return Ok(new ResponseServer
            {
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }
    }
}