using Microsoft.AspNetCore.Mvc;
using ProductServiceApi.Models.Entities;
using ProductServiceApi.Models.ViewModels;
using ProductServiceApi.Services;

namespace ProductServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategory(id);

                if (category != null)
                {
                    return Ok(category);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the category.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();

                if (categories != null)
                {
                    return Ok(categories);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all categories.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Category>> CreateCategory(CategoryViewModel request)
        {
            try
            {
                var category = await _categoryService.CreateCategory(new Category
                {
                    Name = request.Name
                });

                if (category != null)
                {
                    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
                }

                return BadRequest("Failed to create the category.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a category.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> EditCategory(Guid id, CategoryViewModel request)
        {
            try
            {
                var existingCategory = await _categoryService.EditCategory(id, new Category
                {
                    Name = request.Name
                });

                if (existingCategory != null)
                {
                    return Ok(existingCategory);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a category.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(id);

                if (result)
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a category.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
