using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        
        Task<ProductDetails> GetById(int id);
    }

    public interface IProductQueueRepository : IGenericRepository<ProductQueue>
    {
      
       
    }
}
