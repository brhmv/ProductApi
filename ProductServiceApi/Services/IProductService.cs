using ProductServiceApi.Models.ViewModels;
using ProductServiceApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProductServiceApi.Services
{
    public interface IProductService
    {
        Task<Product?> GetProduct(Guid id);

        Task<List<ProductViewModel>> GetAllProducts([FromQuery] string? sortBy = null, [FromQuery] string? filterBy = null);

        Task<Product> CreateProduct(ProductViewModel request);

        Task<Product> EditProduct(Guid id,ProductViewModel request);

        Task<bool> DeleteProduct(Guid id);
    }
}