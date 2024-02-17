using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Data;
using ProductServiceApi.Models.ViewModels;
using ProductServiceApi.Models.Entities;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ProductServiceApi.Services
{
    public class ProductService(AppDbContext appDbContext, IMapper mapper, ILogger<ProductService> logger, IMemoryCache cache) : IProductService
    {
        private readonly IMapper _mapper = mapper;

        private readonly IMemoryCache _cache = cache;

        private readonly AppDbContext _context = appDbContext;

        private readonly ILogger<ProductService> _logger = logger;

        ///////////////////////////////////////////////////////////////////////////////

        public async Task<Product> CreateProduct(ProductViewModel request)
        {
            try
            {
                if (request != null)
                {

                    var newProduct = _mapper.Map<Product>(request);

                    //if (request.ImagePath != null && request.ImagePath.Length > 0)
                    //{
                    //    string filePath = await UploadFileHelper.UploadFile(request.ImagePath);

                    //    newProduct.ImagePath = Path.Combine(Path.GetFileName(filePath));
                    //}

                    _context.Products.Add(newProduct);
                    await _context.SaveChangesAsync();
                    //return _mapper.Map<ProductViewModel>(newProduct);
                    return newProduct;
                }

                return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a product.");
                throw;
            }
        }

        public async Task<Product> EditProduct(Guid id, ProductViewModel request)
        {
            try
            {
                if (request != null)
                {
                    var existingProduct = await _context.Products.FindAsync(id);

                    if (existingProduct != null)
                    {
                        _mapper.Map(request, existingProduct);
                        await _context.SaveChangesAsync();
                        //return _mapper.Map<ProductViewModel>(existingProduct);
                        return existingProduct;
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

        public async Task<Product?> GetProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var addProductViewModel = _mapper.Map<ProductViewModel>(product);

                    //return addProductViewModel;
                    return product;
                }
                return null;
            }
            catch (Exception) { return null!; }
        }

        public async Task<List<ProductViewModel>> GetAllProducts(string? sortBy = null, string? filterBy = null)
        {
            try
            {             
                if (!_cache.TryGetValue("AllProducts", out List<ProductViewModel>? productViewModels))
                {
                    IQueryable<Product> query = _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.Tags);

                    if (!string.IsNullOrWhiteSpace(filterBy))
                    {
                        query = query.Where(p => p.Category.Name.Contains(filterBy));
                    }

                    if (!string.IsNullOrWhiteSpace(sortBy))
                    {
                        switch (sortBy.ToLower())
                        {
                            case "name":
                                query = query.OrderBy(p => p.Name);
                                break;
                            case "price":
                                query = query.OrderBy(p => p.Price);
                                break;
                        }
                    }

                    var products = await query.ToListAsync();
                    productViewModels = products.Select(p => _mapper.Map<ProductViewModel>(p)).ToList();

                    _cache.Set("AllProducts", productViewModels, TimeSpan.FromMinutes(3));
                }

                return productViewModels!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all products.");
                throw;
            }
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                var Item = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (Item is not null)
                {
                    _context.Products.Remove(Item!);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception) { return false; }
        }
    }
}