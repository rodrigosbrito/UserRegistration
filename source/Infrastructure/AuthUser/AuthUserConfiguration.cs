using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.AuthUser
{
    public sealed class AuthConfiguration : IEntityTypeConfiguration<Domain.Entities.AuthUser>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AuthUser> builder)
        {
            builder.ToTable(nameof(Domain.Entities.AuthUser), "dbo");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(entity => entity.Login).HasMaxLength(100).IsRequired();

            builder.Property(entity => entity.Password).HasMaxLength(1000).IsRequired();

            builder.Property(entity => entity.Salt).HasMaxLength(1000).IsRequired();

            builder.Property(entity => entity.Roles).IsRequired();

            builder.HasOne(entity => entity.User).WithOne().HasForeignKey<Domain.Entities.AuthUser>("UserId").IsRequired();

            builder.HasIndex(entity => entity.Login).IsUnique();

            builder.HasIndex(entity => entity.Salt).IsUnique();

            builder.HasIndex("UserId").IsUnique();
        }
    }
}
