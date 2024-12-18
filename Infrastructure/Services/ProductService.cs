using Application.Common;
using Application.Dtos.ProductDtos;
using Application.ServiceInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IFileStorageService _storageService;

        public ProductService(AppDbContext context, IFileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(CreateProductDto productDto,Guid UserId)
        {
            try {
                var ProductId = Guid.NewGuid();

                Product newProduct = new Product()
                {
                    Id = ProductId,
                    UserId= UserId,
                    CreatedAt = DateTime.UtcNow,
                    ProductDescription = productDto.ProductDescription,
                    ProductName = productDto.ProductName

                };

                await _context.Products.AddAsync(newProduct);

                await _storageService.SaveImagesAsync(productDto.Images,ProductId);

                await _context.SaveChangesAsync();

                var response = new ServiceResponse<Product>(true, "Product Created Succesfully");

                response.values.Add(newProduct);

                return response;


            }
            catch(Exception e)  { 

                return new ServiceResponse<Product>(false,$"Error occoured :{e.Message}"); 
            }

            

        }

        public async Task<ServiceResponse<Product>> DeleteProductAsync(Guid ProductId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

            if (product == null )
            {
                return new ServiceResponse<Product>(false, "This Product doesn't Exist");
            }

            _context.Products.Remove(product);

            return new ServiceResponse<Product>(true, "Product Deleted Succesfully");


        }

        public async Task<ServiceResponse<Product>> FindProductByIdAsync(Guid ProductId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

            if (product is null)
            {
                return new ServiceResponse<Product>(false, "This Product doesn't Exist");
            }

            var response = new ServiceResponse<Product>(true, "Product Created Succesfully");


            response.values.Add(product);
            return response;
        }

        public async Task<ServiceResponse<Product>> FindProductByNameAsync(string ProductName)
        {
            List<Product> products = await _context.Products.Where(p => p.ProductName!.Contains(ProductName)).ToListAsync();

            return new ServiceResponse<Product>(true, "Products Found Succesfully", products);
        }

        public async Task<ServiceResponse<Product>> GetAlProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return new ServiceResponse<Product>(true, "Succesful Operation", products);
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(UpdateProductDto productDto)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.ProductId);

            if (product is null) {

                return new ServiceResponse<Product>(false, "This Product doesn't Exist");

            }

            product.ProductName = productDto.ProductName;

            product.ProductDescription = productDto.ProductDescription;

            await _context.SaveChangesAsync();

            var response = new ServiceResponse<Product>(true, "Product Created Succesfully");

            response.values.Add(product);

            return response;
        }
    }
}
