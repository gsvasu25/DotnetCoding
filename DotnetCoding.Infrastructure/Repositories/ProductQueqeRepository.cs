using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductQueqeRepository : GenericRepository<ProductQueue>, IProductQueueRepository
    {
        private readonly DbContextClass _dbContext;
        public ProductQueqeRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

     
        public Task Create(ProductQueue queue)
        {
            _dbContext.ProductQueue.Add(queue);
            return Task.CompletedTask;
        }


        public Task Delete(ProductQueue queue)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductQueue> GetByGuid(string guid)
        {
            var products = _dbContext.ProductQueue.Where(p => p.GUID.ToString() == guid);
            if(products.Any())
            {
                return products.First();
            }
            return null;
        }

      

        public Task Update(ProductQueue queue)
        {
            _dbContext.ProductQueue.Update(queue);
            return Task.CompletedTask;
        }
    }
}
