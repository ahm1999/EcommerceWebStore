using Application.Dtos.ProductDtos;
using Application.ServiceInterfaces;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
       
        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAlProducts();
            return View(response.values);
        }
        [Authorize(Roles = RolesConsts.TRADER)]
        public IActionResult AddProduct() { 
        
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RolesConsts.TRADER)]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto productDto)
        {

            if (!ModelState.IsValid) {
                TempData["Message"] = "Invalid Inputs";
                return RedirectToAction("AddProduct");
            }
            Guid UserId = _userService.GetCurrnetUserId();
            await _productService.CreateProductAsync(productDto,UserId);

            if (UserId == Guid.Empty)
            {
                TempData["Message"] = $"UserId {UserId.ToString()} Is Invalid ";
                return RedirectToAction("AddProduct");
            }

            return RedirectToAction("Index",new { Message = "Product created Succesfully"});
        }
    }
}
