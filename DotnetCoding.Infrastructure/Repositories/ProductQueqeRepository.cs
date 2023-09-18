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
      
      
    }
}
