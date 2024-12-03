
using Application.Common;
using Application.Dtos.UserDtos;
using Application.ServiceInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid GetCurrnetUserId()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> GetUserByIdAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> LogInAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> SignUpAsync(CreateUserDTO createUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
