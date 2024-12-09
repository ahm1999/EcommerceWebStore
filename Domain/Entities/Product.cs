using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }

        [ForeignKey(nameof(User))]
        public Guid AddedBy { get; set; }

        public ICollection<ProductImage>? ProductImages { get; set; }
    }
}
