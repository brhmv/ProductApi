using Microsoft.AspNetCore.Mvc;
using ProductServiceApi.Models.Entities;

namespace ProductServiceApi.Services
{
    public interface ITagService
    {
        Task<ActionResult<List<Tag>>> GetAllTags();

        Task<Tag> CreateTag(string request);

        Task<bool> DeleteTag(Guid id);
    }
}
