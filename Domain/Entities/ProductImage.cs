using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductImage:BaseEntity
    {
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
    }
}
