
using Application.Common;
using Application.Dtos.ProductDtos;
using Domain.Entities;

namespace Application.ServiceInterfaces
{
    public interface IProductService
    {
        
        Task<ServiceResponse<Product>> CreateProductAsync(CreateProductDto productDto,Guid UserId);
        Task<ServiceResponse<Product>> FindProductByIdAsync(Guid ProductId);
        Task<ServiceResponse<Product>> UpdateProductAsync(UpdateProductDto productDto);
        Task<ServiceResponse<Product>> FindProductByNameAsync(string ProductName);
        Task<ServiceResponse<Product>> DeleteProductAsync(Guid Product);

        Task<ServiceResponse<Product>> GetAlProducts();

        

    }
}

