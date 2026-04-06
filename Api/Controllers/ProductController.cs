using System.Net;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : StoreController
    {
        public ProductsController(IStorage storage) : base(storage)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ServerResponse>> Create(
            [FromBody] ProductCreateDto productCreateDto
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // проверка на обязательное наличие изображения
                    if (productCreateDto.Image == null
                        || productCreateDto.Image.Length == 0)
                    {
                        return BadRequest(new ServerResponse
                        {
                            IsSuccess = false,
                            StatusCode = HttpStatusCode.BadRequest,
                            ErrorMessages = { "Наличие изображения (Image) обязательно" }
                        });
                    }
                    else
                    {
                        // демо-вариант с фейковым значением
                        productCreateDto.Image = $"https://placehold.co/100";
                        // Image = productCreateDto.Image // более корректный вариант для финальной версии

                        Product addedProduct = await Task.FromResult(storage.AddProduct(productCreateDto));

                        ServerResponse response = new()
                        {
                            StatusCode = HttpStatusCode.Created,
                            Result = addedProduct
                        };
                        // возврат добавленного значения (по сути - выполнение метода GetById(id))
                        return CreatedAtRoute(nameof(GetById), new { id = addedProduct.Id }, response);
                    }
                }
                else // невалидная модель
                {
                    return BadRequest(new ServerResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { "Невалидная модель данных" }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Возникла ошибка/исключение", ex.Message }
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ServerResponse>> GetAll()
        {
            ServerResponse receivedProducts = new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = await Task.FromResult(storage.GetAllProducts())
            };

            return Ok(receivedProducts);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<ActionResult<ServerResponse>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Указан некорректный ID" }
                });
            }

            Product receivedProduct = await Task.FromResult(storage.GetProduct(id));

            if (receivedProduct == null)
            {
                return NotFound(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = { "Продукт по указанному ID не найден" }
                });
            }
            return Ok(new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = receivedProduct
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServerResponse>> Update(
            int id, [FromBody] ProductUpdateDto productUpdateDto
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (productUpdateDto == null || productUpdateDto.Id != id)
                    {
                        return BadRequest(new ServerResponse
                        {
                            IsSuccess = false,
                            StatusCode = HttpStatusCode.BadRequest,
                            ErrorMessages = { "Несоответствие модели данных" }
                        });
                    }
                    else
                    {
                        Product updatedProduct = await Task.FromResult(storage.UpdateProduct(id, productUpdateDto));

                        if (updatedProduct == null)
                        {
                            return NotFound(new ServerResponse
                            {
                                IsSuccess = false,
                                StatusCode = HttpStatusCode.NotFound,
                                ErrorMessages = { "Продукт по указанному ID не найден" }
                            });
                        }

                        return Ok(new ServerResponse
                        {
                            StatusCode = HttpStatusCode.OK,
                            Result = updatedProduct
                        });
                    }
                }
                else
                {
                    return BadRequest(new ServerResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { "Невалидная модель данных" }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Возникла ошибка/исключение", ex.Message }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResponse>> RemoveById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ServerResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { "Указан некорректный ID" }
                    });
                }

                bool isProductRemoved = await Task.FromResult(storage.RemoveProduct(id));

                if (isProductRemoved == false)
                {
                    return NotFound(new ServerResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessages = { "Продукт по указанному ID не найден" }
                    });
                }

                return Ok(new ServerResponse
                {
                    StatusCode = HttpStatusCode.NoContent
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServerResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "Возникла ошибка/исключение", ex.Message }
                });
            }
        }
    }
}