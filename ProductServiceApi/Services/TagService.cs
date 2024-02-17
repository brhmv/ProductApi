
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductServiceApi.Data;
using ProductServiceApi.Models.Entities;
using ProductServiceApi.Services;

public class TagService(AppDbContext context, IMemoryCache cache) : ITagService
{
    private readonly AppDbContext _context = context;
    private readonly IMemoryCache _cache = cache; 

    public async Task<ActionResult<List<Tag>>> GetAllTags()
    {
        try
        {
            if (!_cache.TryGetValue("AllTags", out List<Tag>? tags))
            {
                tags = await _context.Tags.ToListAsync();

                _cache.Set("AllTags", tags, TimeSpan.FromMinutes(5));
            }

            return tags;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving tags.", ex);
        }
    }

    public async Task<Tag> CreateTag(string request)
    {
        try
        {
            var newTag = new Tag { Name = request };

            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            _cache.Remove("AllTags");

            return newTag;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating a tag.", ex);
        }
    }

    public async Task<bool> DeleteTag(Guid id)
    {
        try
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return false;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            _cache.Remove("AllTags");

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the tag.", ex);
        }
    }

  
}