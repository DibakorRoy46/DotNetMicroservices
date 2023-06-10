using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository productRepository, 
            ILogger<CatalogController> logger)
        {
            _productRepository = productRepository??
                throw new ArgumentNullException(nameof(productRepository));
            _logger = logger??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("Product")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("Product/{productId}", Name ="GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string productId)
        {
            var product=await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                _logger.LogError($"Product with id:{productId} not found ");
                return NotFound();
            }
            return Ok(product); 
        }

        [HttpGet("Product/Name/{productName}", Name = "GetProductByName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByName(string productName)
        {
            var product = await _productRepository.GetProductByNameAsync(productName);
            if (product == null)
            {
                _logger.LogError($"Product with id:{productName} not found ");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("Product/Category/{productCategory}", Name = "GetProductByCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCateogory(string productCategory)
        {
            var product = await _productRepository.GetProductsByCategoryAsync(productCategory);
            if (product == null)
            {
                _logger.LogError($"Product with id:{productCategory} not found ");
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("Product")]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>>CreateProduct([FromBody]Product product)
        {
            await _productRepository.AddProductAsync(product);
            return CreatedAtRoute("GetProduct", new { productId = product.Id }, product);
        }
        [HttpPut("Product")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProductAsync(product));
        }
        [HttpDelete("Product/{productId}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string productId )
        {
            return Ok(await _productRepository.DeleteProductAsync(productId));
        }
    }
}
