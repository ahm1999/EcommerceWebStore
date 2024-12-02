

namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public ICollection<Product>? ProductsByUser { get; set; }
    }
}
