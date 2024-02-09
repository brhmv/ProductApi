using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductServiceApi.Models.Entities;
using ProductServiceApi.Models.ViewModels;
using ProductServiceApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            try
            {
                var item = await _productService.GetProduct(id);

                return item is not null ? Ok(item) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the product.");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery] string? sortBy=null, [FromQuery] string? filterBy = null)
        {
            try
            {
                var products = await _productService.GetAllProducts(sortBy!, filterBy!);

                return products is not null ? Ok(products) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all products.");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                var createdProduct = await _productService.CreateProduct(productViewModel);

                return createdProduct is not null ? Ok(createdProduct) : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> EditProduct(Guid id, [FromBody] ProductViewModel productViewModel)
        {
            try
            {
                var editedProduct = await _productService.EditProduct(id, productViewModel);

                return editedProduct is not null ? Ok(editedProduct) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing the product.");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(Guid id)
        {
            try
            {
                var isDeleted = await _productService.DeleteProduct(id);

                return isDeleted ? Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
