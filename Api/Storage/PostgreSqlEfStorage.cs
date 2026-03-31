using Api.Data;
using Api.Model;

public class PostgreSqlEfStorage : IStorage
{
    protected readonly AppDbContext dbContext;

    public PostgreSqlEfStorage(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Product> GetAllProducts()
    {
        return dbContext.Products.ToList(); // БД.таблица.преобразовать_в_список()
    }
}