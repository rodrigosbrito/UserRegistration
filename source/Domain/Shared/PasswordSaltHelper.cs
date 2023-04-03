using System.Security.Cryptography;
using System.Text;

namespace Domain.Shared
{
    public static class PasswordSaltHelper
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, passwordSaltBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, passwordSaltBytes, passwordBytes.Length, saltBytes.Length);
            byte[] hashBytes = sha256.ComputeHash(passwordSaltBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            string hashedInput = HashPassword(password, salt);
            return hashedInput == hashedPassword;
        }
        public static string CreatePasswordWithSalt(string value, string salt)
        {
            using DeriveBytes deriveBytes = Generate(value, salt);
            return Convert.ToBase64String(deriveBytes.GetBytes(512));
        }

        public static DeriveBytes Generate(string password, string salt)
        {
            return new Rfc2898DeriveBytes(password, Encoding.Default.GetBytes(salt), 10000, HashAlgorithmName.SHA512);
        }
    }
}
