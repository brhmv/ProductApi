using System.ComponentModel.DataAnnotations;

namespace ProductServiceApi.Models.ViewModels
{
    public class CategoryViewModel
    {
        [MinLength(2, ErrorMessage = "The Name field must be at least 2 characters.")]
        [Required(ErrorMessage = "The Description field is required.")]
        public string Name { get; set; } = null!;
    }
}