using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();
        Task<IEnumerable<ProductDetails>> GetAllProducts(Filter filter);
        Task CreateProduct(ProductDetails productDetails);
        Task UpdateProduct(ProductDetails productDetails);
        Task DeleteProduct(string guid);
        Task ProcessQueqe(Process process);
        Task<IEnumerable<ProductQueueDto>> GetPendingProducts();
    }
}
