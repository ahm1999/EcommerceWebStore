using Application.Common;
using Application.Dtos.UserDtos;
using Domain.Entities;
using static Domain.Enums.ProductEnums;


namespace Application.ServiceInterfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetUserByIdAsync(Guid UserId);

        AccountType? GetAccountType();

        bool IsLoggedIn();
        Task<ServiceResponse<User>> SignUpAsync(CreateUserDTO createUserDTO);

        Guid GetCurrnetUserId();
        Task<ServiceResponse<User>> LogInAsync(LogInDTO logInDTO);

        Task<ServiceResponse<User>>  SignOutAsync();

    }
}
