using System.Net;
using Api.Data;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

                        Product productFromDb = await Task.FromResult(storage.AddProduct(productCreateDto));

                        ServerResponse response = new()
                        {
                            StatusCode = HttpStatusCode.Created,
                            Result = productFromDb
                        };
                        // возврат добавленного значения (по сути - выполнение метода GetById(id))
                        return CreatedAtRoute(nameof(GetById), new { id = productFromDb.Id }, response);
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
            ServerResponse response = new ServerResponse
            {
                StatusCode = HttpStatusCode.OK,
                Result = await Task.FromResult(storage.GetAllProducts())
            };

            return Ok(response);
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

            Product result = await Task.FromResult(storage.GetProduct(id));

            if (result == null)
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
                Result = result
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
                        Product productFromDb = await Task.FromResult(storage.GetProduct(id));

                        if (productFromDb == null)
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
                            Result = await Task.FromResult(storage.UpdateProduct(id, productUpdateDto))
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
    }
}