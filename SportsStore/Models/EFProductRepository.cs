﻿namespace SportsStore.Models
{
    public class EFProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EFProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> Products => _dbContext.Products;
    }
}