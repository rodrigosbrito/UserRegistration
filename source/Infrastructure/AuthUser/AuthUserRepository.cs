using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AuthUser
{
    public sealed class AuthUserRepository : IAuthUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.AuthUser authUser) => await _context.AuthUsers.AddAsync(authUser);

        public async Task<bool> LoginExistsAsync(string login) => await _context.AuthUsers.FirstOrDefaultAsync(u => u.Login == login) != null;
    }
}
