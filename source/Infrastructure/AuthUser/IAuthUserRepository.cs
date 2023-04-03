namespace Infrastructure.AuthUser
{
    public interface IAuthUserRepository
    {
        Task<bool> LoginExistsAsync(string login);
        Task AddAsync(Domain.Entities.AuthUser authUser);
    }
}
