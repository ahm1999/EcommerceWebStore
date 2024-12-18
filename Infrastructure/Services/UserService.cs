
using Application.Common;
using Application.Dtos.UserDtos;
using Application.ServiceInterfaces;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Domain.Enums.ProductEnums;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AppDbContext _context;

        public UserService(IHttpContextAccessor contextAccessor, IPasswordHasher passwordHasher,AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public AccountType? GetAccountType()
        {

            if (!IsLoggedIn())
            {
                return null;
            }

            string? role = _contextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            switch (role) {
                case RolesConsts.CLIENT:
                    return AccountType.ClientAccount;
                    
                case RolesConsts.TRADER:
                    return AccountType.TraderAccount;

                default:
                    return null;   

            }
        }

        public Guid GetCurrnetUserId()
        {
            string? retGuid =  _contextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value ;
            if (retGuid is null) {
                return Guid.Empty;
            
            }
            Guid GuidValue = Guid.TryParse(retGuid, out GuidValue) ? GuidValue : Guid.Empty;
            return GuidValue;


        }

        public Task<ServiceResponse<User>> GetUserByIdAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public bool IsLoggedIn()
        {
            Claim? c = _contextAccessor.HttpContext!.User.Claims.FirstOrDefault();
            if (c is null)
            {
                return false;
            }
            return true;
        }

        public async Task<ServiceResponse<User>> LogInAsync(LogInDTO logInDTO)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == logInDTO.UserEmail);
            if (user is null)
            {
                return new ServiceResponse<User>(false, "User Doesn't Exist");
            }

            bool state = _passwordHasher.VerifyHashedPassword(logInDTO.Password!, user.PasswordHash!);

            if (!state)
            {
                return new ServiceResponse<User>(false, "Wrong Password");
            }


            await SignInUser(user);

            return new ServiceResponse<User>(true, "Signed In Succesfully");
        }

        public async Task<ServiceResponse<User>> SignOutAsync()
        {
            await _contextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new ServiceResponse<User>(true, "Signed Out Succesfully");
        }

        public async Task<ServiceResponse<User>> SignUpAsync(CreateUserDTO createUserDTO)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == createUserDTO.UserEmail);

            if (user is not null)
            {
                return new ServiceResponse<User>(false, "User Already Exists With that Email");
            }

            string passwordHash = _passwordHasher.hashPassword(createUserDTO.Password!);

            User new_user = new()
            {
                Id = Guid.NewGuid(),
                UserName = createUserDTO.UserName,
                UserEmail = createUserDTO.UserEmail,
                CreatedAt = DateTime.UtcNow,
                AccountType = createUserDTO.AccountType,
                PasswordHash = passwordHash

            };

            await _context.Users.AddAsync(new_user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<User>(true, "User Added Succesfully");

        }

        private async Task SignInUser(User user) {

            string role = "";
            switch (user.AccountType) {

                case AccountType.ClientAccount:
                    role = RolesConsts.CLIENT;
                    break;

                case AccountType.TraderAccount:
                    role = RolesConsts.TRADER;
                    break;


            }

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserEmail!),
                new Claim("FullName", user.UserName !),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                
            };

            await _contextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);



        }
    }
}
