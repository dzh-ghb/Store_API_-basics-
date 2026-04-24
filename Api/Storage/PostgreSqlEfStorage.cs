using Api.Common;
using Api.Data;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Identity;

public class PostgreSqlEfStorage : IStorage
{
    protected readonly AppDbContext dbContext;

    public PostgreSqlEfStorage(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #region ProductsInfrastructure

    public Product AddProduct(ProductCreateDto productCreateDto)
    {
        Product item = new()
        {
            Name = productCreateDto.Name,
            Description = productCreateDto.Description,
            SpecialTag = productCreateDto.SpecialTag,
            Category = productCreateDto.Category,
            Price = productCreateDto.Price,
            // Image = productCreateDto.Image // более корректный вариант для финальной версии
            Image = $"https://placehold.co/100" // демо-вариант с фейковым значением
        };

        // добавление в БД
        dbContext.Products.Add(item);
        dbContext.SaveChanges(); // id из БД будет добавлен в item после обработки операции Entity Framework

        return item;
    }

    public List<Product> GetAllProducts()
    {
        return dbContext.Products.ToList(); // БД.таблица.преобразовать_в_список()
    }

    public Product GetProduct(int id)
    {
        return dbContext.Products.FirstOrDefault(x => x.Id == id);
    }

    public Product UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        Product item = GetProduct(id);

        if (item == null)
        {
            return null;
        }

        item.Name = productUpdateDto.Name; // поле обязательное, проверка не нужна
        item.Description = productUpdateDto.Description;
        if (!String.IsNullOrEmpty(productUpdateDto.SpecialTag))
        {
            item.SpecialTag = productUpdateDto.SpecialTag;
        }
        if (!String.IsNullOrEmpty(productUpdateDto.Category))
        {
            item.Category = productUpdateDto.Category;
        }
        item.Price = productUpdateDto.Price;
        if (productUpdateDto.Image != null
            && productUpdateDto.Image.Length > 0)
        {
            item.Image = $"https://placehold.co/200";
        }

        dbContext.Products.Update(item);
        dbContext.SaveChanges();

        return item;
    }

    public bool RemoveProduct(int id)
    {
        Product item = GetProduct(id);

        if (item == null)
        {
            return false;
        }

        dbContext.Products.Remove(item);
        dbContext.SaveChanges();

        return true;
    }

    #endregion

    #region AuthInfrastructure

    public async Task<bool> AddUser(RegisterRequestDto registerRequestDto, UserManager<AppUser> userManager/*, RoleManager<IdentityRole> roleManager*/)
    {
        AppUser user = new AppUser
        {
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
            // NormalizedEmail = registerRequestDto.Email.ToUpper(),
            FirstName = registerRequestDto.UserName
        };

        // попытка создания юзера
        var result = await userManager.CreateAsync(user, registerRequestDto.Password);

        if (!result.Succeeded)
        {
            return false;
        }

        // определение указанной роли
        var role = registerRequestDto.Role.Equals(
            SharedData.Roles.Admin, StringComparison.OrdinalIgnoreCase)
            ? SharedData.Roles.Admin
            : SharedData.Roles.Consumer;

        // привязка юзера к роли
        await userManager.AddToRoleAsync(user, role);

        return true;
    }

    public AppUser GetUser(RegisterRequestDto registerRequestDto)
    {
        return dbContext
        .AppUsers
        .FirstOrDefault(u => u.UserName.ToLower() == registerRequestDto.UserName.ToLower());
    }

    #endregion
}