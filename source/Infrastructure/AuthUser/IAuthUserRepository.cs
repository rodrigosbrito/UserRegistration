namespace Infrastructure.AuthUser
{
    public interface IAuthUserRepository
    {
        Task<bool> LoginExistsAsync(string login);
        Task<Domain.Entities.AuthUser> GetByEmailOrLogin(string login);
        Task AddAsync(Domain.Entities.AuthUser authUser);
    }
}
