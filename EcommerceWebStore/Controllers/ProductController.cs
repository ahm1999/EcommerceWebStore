using Application.ServiceInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            List<Product> products = new() {

            new Product() {
                Id = Guid.NewGuid(),
                AddedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductDescription = "THis is product desctiption",
                ProductName = "Product 1"


                }
            ,new Product() {
                Id = Guid.NewGuid(),
                AddedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductDescription = "THis is product desctiption",
                ProductName = "Product 2"


            },
            new Product() {
                Id = Guid.NewGuid(),
                AddedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductDescription = "THis is product desctiption",
                ProductName = "Product 3"


                }



            };
            return View(products);
        }

        public IActionResult AddProduct() { 
        
            return View();
        }
    }
}
