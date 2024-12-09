
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        [Required]
        [MinLength(3)]
        [StringLength(40)]
        public string? ProductName { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(100)]

        public string? ProductDescription { get; set; }

        [Required]
        
        public ICollection<IFormFile>? Images { get; set; }
       // public Guid AddedBy { get; set; }
    }
}
