

namespace Application.ServiceInterfaces
{
    public interface IPasswordHasher
    {
        public string hashPassword(string userInputPassword);
        public bool VerifyHashedPassword(string userInputPassword,string HashFromDB);
    }
}
