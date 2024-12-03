
using static Domain.Enums.ProductEnums;

namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? PasswordHash { get; set; }

        public AccountType AccountType { get; set; }
        public ICollection<Product>? ProductsByUser { get; set; }
    }
}
