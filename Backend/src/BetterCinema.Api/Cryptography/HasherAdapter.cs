using BCrypt.Net;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BetterCinema.Api.Cryptography
{
    public interface IHasherAdapter
    {
        string HashText(string text);
        bool VerifyHashedText(string plainText, string hashedPassword);
    }
    public class HasherAdapter : IHasherAdapter
    {
        public string HashText(string text)
        {
            return BCryptNet.HashPassword(text);
        }

        public bool VerifyHashedText(string plainText, string hashedText)
        {
            var doesMatch = false;
            try
            {
                doesMatch = BCryptNet.Verify(plainText, hashedText);
            }
            catch (SaltParseException e) {}
            return doesMatch;
        }
    }
}
