using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        private Response _response;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
            _response=new Response();
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var productDetailsList = await _productService.GetAllProducts();
                if (productDetailsList == null)
                {
                    return NotFound("Details not found");
                }
                return Ok(productDetailsList);
            }
            catch(Exception ex)
            {
                throw new Exception("Server Error");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDetailsDto productDetailsDto)
        {
            if (productDetailsDto.Price <= 0)
            {
                throw new Exception("Price should greater than zero");
            }
            if (productDetailsDto.Price > 10000)
            {
                throw new Exception("Price should not greater than 10000");
            }
            var productDetails = new ProductDetails()
            {
                CreatedTimeUtc = DateTime.UtcNow,
                UpdateTimeUtc = DateTime.UtcNow,
                Name = productDetailsDto.Name,
                Price = productDetailsDto.Price,
                Description = productDetailsDto.Description
            };
            await _productService.CreateProduct(productDetails);
            return Ok("Product Added");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDetailsDto productDetailsDto)
        {
            if (productDetailsDto.Price <= 0)
            {
                throw new Exception("Price should greater than zero");
            }
            if (productDetailsDto.Price > 10000)
            {
                throw new Exception("Price should not greater than 10000");
            }
            var productDetails = new ProductDetails()
            {               
                UpdateTimeUtc = DateTime.UtcNow,
                Name = productDetailsDto.Name,
                Price = productDetailsDto.Price,
                Description = productDetailsDto.Description,
                GUID=productDetailsDto.Guid
            };
            await _productService.UpdateProduct(productDetails);
            return Ok("Product Updated");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string guid)
        {            
            
            await _productService.DeleteProduct(guid);
            return Ok("Product Delete");
        }

        [HttpGet]
        [Route("Pending")]
        public async Task<IActionResult> GetPendingProducts()
        {
            var productDetailsList = await _productService.GetPendingProducts();
          
            return Ok(productDetailsList);
        }
        [HttpPost]
        [Route("Process")]
        public async Task<IActionResult> ProcessQueqe([FromBody] Process process)
        {
             await _productService.ProcessQueqe(process);

            return Ok();
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetProductList([FromBody] Filter fIlter)
        {
            var productDetailsList = await _productService.GetAllProducts(fIlter);

            return Ok(productDetailsList);
        }
    }
}
