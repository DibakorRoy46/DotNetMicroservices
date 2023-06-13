using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryRepository categoryRepository,
            ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("Product/Category")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetProducts()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("Product/Category/{categoryId}", Name = "GetCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetProductById(string categoryId)
        {
            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            if (category == null)
            {
                _logger.LogError($"Category with id:{categoryId} not found ");
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("Product/Category")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, category);
        }
    }
}
