namespace Application.Jwt
{
    public interface IJwtProvider
    {
        string Generate(Domain.Entities.AuthUser authUser);
    }
}
