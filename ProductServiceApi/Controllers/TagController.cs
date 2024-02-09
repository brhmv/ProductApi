using Microsoft.AspNetCore.Mvc;
using ProductServiceApi.Models.Entities;
using ProductServiceApi.Services;

namespace ProductServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController(ITagService tagService) : ControllerBase
    {
        private readonly ITagService _tagService = tagService;

        [HttpGet("All")]
        public async Task<ActionResult<List<Tag>>> GetAllTags()
        {
            try
            {
                var tags = await _tagService.GetAllTags();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}.An error occurred while creating the tag.");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Tag>> CreateTag(string tagName)
        {
            try
            {
                var newTag = await _tagService.CreateTag(tagName);
                return Ok(newTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}.An error occurred while creating the tag.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTag(Guid id)
        {
            try
            {
                var isDeleted = await _tagService.DeleteCategory(id);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}.An error occurred while creating the tag.");
            }
        }
    }
}