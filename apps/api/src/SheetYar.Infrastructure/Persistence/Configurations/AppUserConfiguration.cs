using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Users;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ConfigureBaseEntity("AppUsers");

        builder.Property(user => user.UserName).HasMaxLength(320);
        builder.Property(user => user.NormalizedUserName).HasMaxLength(320);
        builder.Property(user => user.Email).HasMaxLength(320).IsRequired();
        builder.Property(user => user.NormalizedEmail).HasMaxLength(320).IsRequired();
        builder.Property(user => user.PasswordHash).HasMaxLength(1024).IsRequired();
        builder.Property(user => user.SecurityStamp).HasMaxLength(64);
        builder.Property(user => user.ConcurrencyStamp).HasMaxLength(64);
        builder.Property(user => user.PhoneNumber).HasMaxLength(32);
        builder.Property(user => user.DisplayName).HasMaxLength(120).IsRequired();

        builder.HasIndex(user => user.NormalizedEmail).IsUnique();
        builder.HasIndex(user => new { user.IsActive, user.CreatedAtUtc });
    }
}
