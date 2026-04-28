using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Identity;

public interface IStorage
{
    #region ProductsInfrastructure
    Product AddProduct(ProductCreateDto productCreateDto);

    List<Product> GetAllProducts();

    Product GetProduct(int id);

    Product UpdateProduct(int id, ProductUpdateDto productUpdateDto);

    bool RemoveProduct(int id);
    #endregion

    #region AuthInfrastructure
    Task<bool> AddUser(RegisterRequestDto registerRequestDto, UserManager<AppUser> userManager/*, RoleManager<IdentityRole> roleManager*/);

    AppUser GetUser(IRequestDto requestDto);

    // AppUser GetLoginnedUser(LoginRequestDto loginRequestDto);

    #endregion
}