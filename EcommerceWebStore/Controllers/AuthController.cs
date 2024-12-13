using Application.Dtos.UserDtos;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult LogIn() {
            
            return View();
        }

        public IActionResult SignUp() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpPost([FromForm]CreateUserDTO createUserDto) {

            _logger.LogInformation("1");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("2");

                TempData["Message"] = "Validation Errrors";
                return RedirectToAction("SignUp");
            }

            var Response =  await _userService.SignUpAsync(createUserDto);

            if (!Response.status)
            {
                TempData["Message"] = Response.Message;
                _logger.LogInformation("3");

                return RedirectToAction("SignUp");
            }

            return RedirectToAction("LogIn");


        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDTO logInDTO) {

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid Inputs ";
                return RedirectToAction("LogIn");
            }


            var result =  await _userService.LogInAsync(logInDTO);

            if (!result.status)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("LogIn");
            }

            return RedirectToAction("Index","Product");


        }


    }
}
