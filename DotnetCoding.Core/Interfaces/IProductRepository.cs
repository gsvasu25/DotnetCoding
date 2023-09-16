using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task Create(ProductDetails productDetails);
        Task Update(ProductDetails productDetails);
        Task Delete(int productId);
        Task<ProductDetails> GetByGuid(string guid);
        Task<ProductDetails> GetById(int id);
    }

    public interface IProductQueueRepository : IGenericRepository<ProductQueue>
    {
        Task Create(ProductQueue queue);
        Task Update(ProductQueue queue);
        Task Delete(ProductQueue queue);
        Task<ProductQueue> GetByGuid(string guid);
    }
}
