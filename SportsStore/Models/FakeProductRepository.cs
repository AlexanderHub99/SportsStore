namespace SportsStore.Models;

public class FakeProductRepository : IProductRepository
{
    public IEnumerable<Product> Products => new List<Product>
    {
        new Product {Name = "P1", Price = 65.73M},
        new Product {Name = "P2", Price = 165.7M},
        new Product {Name = "P3", Price = 425.7M},
        new Product {Name = "P4", Price = 485.74M},
    };
}