using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.User
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable(nameof(Domain.Entities.User), "dbo");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(entity => entity.Name).HasMaxLength(250).IsRequired();

            builder.Property(entity => entity.Email).HasMaxLength(250).IsRequired();

            builder.HasIndex(entity => entity.Email).IsUnique();
        }
    }
}
