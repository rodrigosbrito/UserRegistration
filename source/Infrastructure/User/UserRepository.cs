using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.User
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email) 
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email) != null;
        public async Task AddAsync(Domain.Entities.User user) 
            => await _context.Users.AddAsync(user);
        public async Task<Domain.Entities.User> GetByIdAsync(int id) 
            => await _context.Users.FindAsync(id);
        public async Task<Domain.Entities.User> GetByEmailAsync(string email) 
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    }
}
