using BetterCinema.Api.Contracts.Auth;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BetterCinema.Api.Cryptography
{
    public interface IHasherAdapter
    {
        string HashPassword(string password);
        bool VerifyPassword(string plainTextPassword, string hashedPassword);
    }
    public class HasherAdapter : IHasherAdapter
    {
        public string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCryptNet.Verify(plainTextPassword, hashedPassword);
        }
    }
}
