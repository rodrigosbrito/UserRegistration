namespace Infrastructure.AuthUser
{
    public interface IAuthUserRepository
    {
        Task<bool> LoginExistsAsync(string login, CancellationToken cancellationToken);
        Task<Domain.Entities.AuthUser> GetByEmailOrLogin(string login, CancellationToken cancellationToken);
        Task AddAsync(Domain.Entities.AuthUser authUser, CancellationToken cancellationToken);
    }
}
