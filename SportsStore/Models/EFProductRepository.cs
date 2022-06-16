namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EFProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
            if (!_dbContext.Products.Any())
            {
                 SeedData.EnsrurePopulated(dbContext);
            }
        }

        public IEnumerable<Product?> Products => _dbContext.Products;
    }
}
