namespace SportsStore.Models;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; }
}