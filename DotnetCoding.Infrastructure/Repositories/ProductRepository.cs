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

        public Task Create(ProductDetails productDetails)
        {           
           _dbContext.Products.Add(productDetails);
            return Task.CompletedTask;
        }

        public async Task Delete(int productId)
        {
            var product =await GetById(productId);
            if (product!=null)
            {
                _dbContext.Products.Remove(product);
            }           
        }

        public async Task<ProductDetails> GetByGuid(string guid)
        {
            var products = _dbContext.Products.Where(p => p.GUID.ToString() == guid);
            if(products.Any())
            {
                return products.First();
            }
            return null;
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
        public Task Update(ProductDetails productDetails)
        {
            _dbContext.Products.Update(productDetails);
            return Task.CompletedTask;

        }
    }
}
