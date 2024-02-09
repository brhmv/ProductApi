using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Data;
using ProductServiceApi.Models.Entities;

namespace ProductServiceApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<CategoryService> _logger;

        public CategoryService(AppDbContext appDbContext, ILogger<CategoryService> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }

        public async Task<Category> CreateCategory(Category request)
        {
            try
            {
                if (request != null)
                {
                    _context.Categories.Add(request);
                    await _context.SaveChangesAsync();
                    return request;
                }

                return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a product.");
                throw;
            }
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            try
            {
                var Item = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if (Item is not null)
                {
                    _context.Categories.Remove(Item!);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception) { return false; }
        }

        public async Task<Category> EditCategory(Guid id, Category request)
        {
            try
            {
                if (request != null)
                {
                    var existingCategory = await _context.Categories.FindAsync(id);

                    if (existingCategory != null)
                    {
                        existingCategory.Name = request.Name;
                        await _context.SaveChangesAsync();
                        return existingCategory;
                    }
                }

                return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a product.");
                throw;
            }
        }

        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                return categories == null || categories.Count == 0 ? null! : categories;
            }
            catch (Exception) { return null!; }
        }

        public async Task<Category?> GetCategory(Guid id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                return category ?? null;
            }
            catch (Exception) { return null!; }
        }
    }
}