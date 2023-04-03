using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }
        public DbSet<Domain.Entities.User> Users { get; set; }
        public DbSet<Domain.Entities.AuthUser> AuthUsers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly).Seed();
    }
}
