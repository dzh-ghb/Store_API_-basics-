using Api.Model;

public interface IStorage
{
    List<Product> GetAllProducts();

    Product GetProduct(int id);
}