namespace UserRegistration.Infrastructure.Jwt
{
    public interface IJwtProvider
    {
        string Generate(UserRegistration.Domain.Entities.AuthUser authUser);
    }
}
