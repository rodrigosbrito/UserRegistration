namespace Infrastructure.User
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(Domain.Entities.User user);
        Task<Domain.Entities.User> GetByIdAsync(int id);
        Task<Domain.Entities.User> GetByEmailAsync(string email);
    }
}
