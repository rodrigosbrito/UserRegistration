namespace UserRegistration.Infrastructure.Security
{
    public interface ICryptographyService
    {
        string GenerateHashPassword(string password, string salt);
    }
}
