
using System.ComponentModel.DataAnnotations;
using static Domain.Enums.ProductEnums;

namespace Application.Dtos.UserDtos
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(3)]
        [StringLength(20)]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }

        [Required]
        [MinLength(8)]
        [StringLength(20)]
        public string? Password { get; set; }
        public AccountType AccountType { get; set; } = AccountType.ClientAccount;

    }
}
