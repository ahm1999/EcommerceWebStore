using Application.Dtos.ProductDtos;
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
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAlProducts();
            return View(response.values);
        }

        public IActionResult AddProduct() { 
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto productDto)
        {

            if (!ModelState.IsValid) {
                TempData["Message"] = "Invalid Inputs";
                return RedirectToAction("AddProduct");
            }

            await _productService.CreateProductAsync(productDto,Guid.NewGuid());

            return RedirectToAction("Index",new { Message = "Product created Succesfully"});
        }
    }
}
