namespace Infrastructure.Jwt
{
    public interface IJwtProvider
    {
        string Generate(Domain.Entities.AuthUser authUser);
    }
}
