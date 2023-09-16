using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
using DotnetCoding.Services.Interfaces;
using System.Linq;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList.Where(p => p.IsActive == true).OrderByDescending(p => p.UpdateTimeUtc);
        }
        public async Task<IEnumerable<ProductDetails>> GetAllProducts(Filter filter)
        {
            if (filter.toPrice < filter.fromPrice) throw new Exception("From Price should greaterthan To Price");
            if (filter.toDate < filter.fromDate) throw new Exception("From Date should greaterthan To Date");
            var productDetailsList = await _unitOfWork.Products.GetAll();
            var filteredProducts = productDetailsList
         .Where(p =>
             (string.IsNullOrEmpty(filter.ProductName) || p.Name.Contains(filter.ProductName)) &&
             (!filter.fromPrice.HasValue || p.Price >= filter.fromPrice.Value) &&
             (!filter.toPrice.HasValue || p.Price <= filter.toPrice.Value) &&
             (!filter.fromDate.HasValue || p.UpdateTimeUtc >= filter.fromDate.Value) &&
             (!filter.toDate.HasValue || p.UpdateTimeUtc <= filter.toDate.Value) &&
             p.IsActive == true)
         .OrderByDescending(p => p.UpdateTimeUtc);

            return filteredProducts.ToList();
        }
        public async Task<IEnumerable<ProductQueueDto>> GetPendingProducts()
        {
            var productDetailsList = await _unitOfWork.ProductQueqe.GetAll();

            var productQueueDtoTasks = productDetailsList
                .Where(product => product.StatusId == (int)StatusEnum.Pending)
                .Select(async p =>
                {
                    double price = 0;
                    if (p.UpdateTypeId == (int)UpdateTypeEnum.MoreThan50Percent)
                    {
                        var product = await _unitOfWork.Products.GetById(p.ProductId);
                        price = product.Price;
                    }
                    UpdateTypeEnum updateType = (UpdateTypeEnum)p.UpdateTypeId;
                    return new ProductQueueDto()
                    {
                        UpdateType = updateType.ToString(),
                        Name = p.Name,
                        Description = p.Description,
                        Price = price,
                        NewPrice = p.Price,
                        Guid = p.GUID.ToString()
                    };
                });

            var productQueueDtoArray = await Task.WhenAll(productQueueDtoTasks);
            return productQueueDtoArray.ToList();
        }

        public async Task ProcessQueqe(Process process)
        {
            var queqe = await _unitOfWork.ProductQueqe.GetByGuid(process.Guid);
            if (queqe != null)
            {
                _unitOfWork.Detach(queqe);
                if (!process.Approved)
                {
                    queqe.StatusId = (int)StatusEnum.Rejected;
                    await _unitOfWork.ProductQueqe.Update(queqe);
                    _unitOfWork.Save();
                    return;
                }

                if (queqe.UpdateTypeId == (int)UpdateTypeEnum.Delete)
                {
                    await _unitOfWork.Products.Delete(queqe.ProductId);
                    _unitOfWork.Save();
                    queqe.StatusId = (int)StatusEnum.Approved;
                    await _unitOfWork.ProductQueqe.Update(queqe);
                   
                }
                if (queqe.UpdateTypeId == (int)UpdateTypeEnum.MoreThan5000Price || queqe.UpdateTypeId == (int)UpdateTypeEnum.MoreThan50Percent)
                {
                    var product = await _unitOfWork.Products.GetById(queqe.ProductId);
                    product.Price = queqe.Price;
                    product.UpdateTimeUtc = queqe.UpdateTimeUtc;
                    product.IsActive = true;
                    await _unitOfWork.Products.Update(product);
                    _unitOfWork.Save();
                    queqe.StatusId = (int)StatusEnum.Approved;
                    await _unitOfWork.ProductQueqe.Update(queqe);
                   
                }
                _unitOfWork.Save();
            }
            else
            {
                throw new Exception("Invalid Process Key");
            }
        }
        public async Task CreateProduct(ProductDetails productDetails)
        {           
            if (productDetails.Price > 5000)
            {
                productDetails.IsActive = false;
            }
            else
            {
                productDetails.IsActive = true;
            }
            await _unitOfWork.Products.Create(productDetails);           
            _unitOfWork.Save();
            var product = await _unitOfWork.Products.GetByGuid(productDetails.GUID.ToString());
            if (product != null && !productDetails.IsActive)
            {
                var productQueue = new ProductQueue()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    StatusId = (int)StatusEnum.Pending,
                    UpdateTypeId = (int)UpdateTypeEnum.MoreThan5000Price,
                    Description = product.Description,
                    CreatedTimeUtc = product.CreatedTimeUtc,
                    UpdateTimeUtc = product.UpdateTimeUtc,
                    Price = product.Price,
                };
                await _unitOfWork.ProductQueqe.Create(productQueue);
                _unitOfWork.Save();
            }
        }
        public async Task DeleteProduct(string productId)
        {
            var product = await _unitOfWork.Products.GetByGuid(productId);
            if (product != null)
            {
                var productQueue = new ProductQueue()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    StatusId = (int)StatusEnum.Pending,
                    UpdateTypeId = (int)UpdateTypeEnum.Delete,
                    Description = product.Description,
                };
                await _unitOfWork.ProductQueqe.Create(productQueue);
                _unitOfWork.Save();
            }

        }

        public async Task UpdateProduct(ProductDetails productDetails)
        {
            var product = await _unitOfWork.Products.GetByGuid(productDetails.GUID.ToString());
            if (product != null)
            {
                _unitOfWork.Detach(product);
                if (productDetails.Price >= 2 * product.Price|| productDetails.Price>5000)
                {
                    int type = productDetails.Price > 5000 ? (int)UpdateTypeEnum.MoreThan5000Price : (int)UpdateTypeEnum.MoreThan50Percent;
                    var productQueue = new ProductQueue()
                    {
                        ProductId = product.Id,
                        Name = productDetails.Name,
                        StatusId = (int)StatusEnum.Pending,
                        UpdateTypeId = type,
                        Description = productDetails.Description,
                        Price = productDetails.Price,
                    };

                    await _unitOfWork.ProductQueqe.Create(productQueue);
                }
                else
                {
                    productDetails.Id = product.Id;
                    await _unitOfWork.Products.Update(productDetails);
                }
               
                _unitOfWork.Save();
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
    }
}
