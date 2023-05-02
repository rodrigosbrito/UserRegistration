using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.AuthUser
{
    public sealed class AuthUserRepository : IAuthUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.AuthUser authUser, CancellationToken cancellationToken) 
            => await _context.AuthUsers.AddAsync(authUser, cancellationToken);

        public async Task<bool> LoginExistsAsync(string login, CancellationToken cancellationToken) 
            => await _context.AuthUsers
            .FirstOrDefaultAsync(u => u.Login == login, cancellationToken) is not null;

        public async Task<Domain.Entities.AuthUser> GetByEmailOrLogin(string login, CancellationToken cancellationToken) 
            => await _context.AuthUsers
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.Login == login || u.User.Email == login, cancellationToken);
    }
}
