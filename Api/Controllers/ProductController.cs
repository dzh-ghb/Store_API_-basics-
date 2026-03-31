using Api.Data;
using Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductController : StoreController
    {
        public ProductController(IStorage storage) : base(storage)
        {
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            // return Ok(await Task.FromResult<string>("_DZHITS"));
            return Ok(await Task.FromResult(storage.GetAllProducts()));
        }
    }
}