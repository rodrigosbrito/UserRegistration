using System.Security.Cryptography;
using System.Text;

namespace UserRegistration.Infrastructure.Security
{
    public sealed class CryptographyService : ICryptographyService
    {
        public string GenerateHashPassword(string password, string salt)
        {
            using DeriveBytes deriveBytes = Generate(password, salt);
            return Convert.ToBase64String(deriveBytes.GetBytes(512));
        }
        private DeriveBytes Generate(string password, string salt) 
            => new Rfc2898DeriveBytes(
                password, 
                Encoding.Default.GetBytes(salt), 
                10000, 
                HashAlgorithmName.SHA512);
    }
}
