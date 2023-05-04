using Domain.Model;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Threading;

namespace Infrastructure.User
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public static Expression<Func<Domain.Entities.User, UserModel>> Model => user => new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken) 
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken) is not null;
        public async Task AddAsync(Domain.Entities.User user, CancellationToken cancellationToken) 
            => await _context.Users.AddAsync(user, cancellationToken);
        public async Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken)
            => await _context.Users.Where(u => u.Id == id).Select(Model).SingleOrDefaultAsync(cancellationToken);
        public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken) 
            => await _context.Users.Where(u => u.Email == email).Select(Model).SingleOrDefaultAsync(cancellationToken);

    }
}
