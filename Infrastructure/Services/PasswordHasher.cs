
using Application.ServiceInterfaces;

using BCrypt;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string hashPassword(string userInputPassword)
        {
            return BC.HashPassword(userInputPassword);
        }

        public bool VerifyHashedPassword(string userInputPassword, string HashFromDB)
        {
            return BC.Verify(userInputPassword, HashFromDB);
        }
    }
}
