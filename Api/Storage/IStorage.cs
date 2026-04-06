using Api.Model;
using Api.ModelDto;

public interface IStorage
{
    Product AddProduct(ProductCreateDto productCreateDto);

    List<Product> GetAllProducts();

    Product GetProduct(int id);

    Product UpdateProduct(int id, ProductUpdateDto productUpdateDto);

    bool RemoveProduct(int id);
}