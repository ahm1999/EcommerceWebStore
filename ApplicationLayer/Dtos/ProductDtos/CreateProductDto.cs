
namespace Application.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public Guid AddedBy { get; set; }
    }
}
