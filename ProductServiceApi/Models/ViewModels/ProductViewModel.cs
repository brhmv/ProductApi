using ProductServiceApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductServiceApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [MinLength(2, ErrorMessage = "The Name field must be at least 2 characters.")]
        [Required(ErrorMessage = "The Description field is required.")]
        public string Name { get; set; } = null!;

        
        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The field 0 must be greater than or equal to 1.")]
        public double Price { get; set; }

                
        [MinLength(3, ErrorMessage = "The Name field must be at least 3 characters.")]
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; } = null!;

               
        [MinLength(5, ErrorMessage = "The Name field must be at least 5 characters.")]
        [Required(ErrorMessage = "The ImagePath field is required.")]
        public string ImagePath { get; set; } = null!;

        
        [Required(ErrorMessage = "The Category field is required.")]
        public Guid CategoryId { get; set; }
        
        public List<string>? TagsName { get; set; }

        public List<string>? CategoriesName { get; set; } 
    }
}