using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Data;
using ProductServiceApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceApi.Services
{
    public class TagService(AppDbContext context) : ITagService
    {
        private readonly AppDbContext _context = context;

        public async Task<Tag> CreateTag(string request)
        {
            try
            {
                var newTag = new Tag { Name = request };

                _context.Tags.Add(newTag);
                await _context.SaveChangesAsync();

                return newTag;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a tag.", ex);
            }
        }

        public async Task<bool> DeleteCategory(Guid id)
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

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the tag.", ex);
            }
        }

        public async Task<ActionResult<List<Tag>>> GetAllTags()
        {
            try
            {
                var tags = await _context.Tags.ToListAsync();

                return tags;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving tags.", ex);
            }
        }
    }
}
