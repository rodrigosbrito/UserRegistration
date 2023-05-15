namespace UserRegistration.Infrastructure.AuthUser
{
    public interface IAuthUserRepository
    {
        Task<bool> LoginExistsAsync(string login, CancellationToken cancellationToken);
        Task<UserRegistration.Domain.Entities.AuthUser> GetByEmailOrLogin(string login, CancellationToken cancellationToken);
        Task AddAsync(UserRegistration.Domain.Entities.AuthUser authUser, CancellationToken cancellationToken);
    }
}
