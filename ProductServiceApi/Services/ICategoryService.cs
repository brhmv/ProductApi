using Microsoft.AspNetCore.Mvc;
using ProductServiceApi.Models.Entities;

namespace ProductServiceApi.Services
{
    public interface ICategoryService
    {
        Task<Category?> GetCategory(Guid id);

        Task<ActionResult<List<Category>>> GetAllCategories();

        Task<Category> CreateCategory(Category request);

        Task<Category> EditCategory(Guid id, Category request);

        Task<bool> DeleteCategory(Guid id);
    }
}