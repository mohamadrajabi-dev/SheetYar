using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Users;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ConfigureBaseEntity("AppUsers");

        builder.Property(user => user.Email).HasMaxLength(320).IsRequired();
        builder.Property(user => user.NormalizedEmail).HasMaxLength(320).IsRequired();
        builder.Property(user => user.PasswordHash).HasMaxLength(1024).IsRequired();
        builder.Property(user => user.DisplayName).HasMaxLength(120).IsRequired();

        builder.HasIndex(user => user.NormalizedEmail).IsUnique();
        builder.HasIndex(user => new { user.IsActive, user.CreatedAtUtc });
    }
}
