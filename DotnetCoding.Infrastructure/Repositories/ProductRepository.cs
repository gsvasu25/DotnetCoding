using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using System;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        private readonly DbContextClass _dbContext;
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task<ProductDetails> GetById(int id)
        {
            var products = _dbContext.Products.Where(p => p.Id == id);
            if (products.Any())
            {
                return products.First();
            }
            return null;
        }
      
    }
}
