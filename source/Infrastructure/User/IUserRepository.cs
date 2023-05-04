using Domain.Model;

namespace Infrastructure.User
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
        Task AddAsync(Domain.Entities.User user, CancellationToken cancellationToken);
        Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
