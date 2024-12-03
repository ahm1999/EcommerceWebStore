using Application.Common;
using Application.Dtos.UserDtos;
using Domain.Entities;


namespace Application.ServiceInterfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetUserByIdAsync(Guid UserId);
        

        Task<ServiceResponse<User>> SignUpAsync(CreateUserDTO createUserDTO);

        Guid GetCurrnetUserId();
        Task<ServiceResponse<User>> LogInAsync(User user);

    }
}
