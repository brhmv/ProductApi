namespace ProductServiceApi.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public string Description { get; set; } = null!;

        public string ImagePath { get; set; } = null!;

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public List<Tag> Tags { get; set; } = null!;
    }
}