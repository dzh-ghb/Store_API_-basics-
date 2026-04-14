using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StoreController : ControllerBase
    {
        protected readonly IStorage storage;

        public StoreController(IStorage storage)
        {
            this.storage = storage;
        }
    }
}