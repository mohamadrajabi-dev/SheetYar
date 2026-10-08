using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SheetYar.Domain.Authentication;

namespace SheetYar.Infrastructure.Persistence.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ConfigureBaseEntity(
            "RefreshTokens",
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_RefreshTokens_ExpiresAtUtc_UTC",
                    "DATEPART(TZOFFSET, [ExpiresAtUtc]) = 0");
                tableBuilder.HasCheckConstraint(
                    "CK_RefreshTokens_RevokedAtUtc_UTC",
                    "[RevokedAtUtc] IS NULL OR DATEPART(TZOFFSET, [RevokedAtUtc]) = 0");
                tableBuilder.HasCheckConstraint(
                    "CK_RefreshTokens_ExpiresAfterCreated",
                    "[ExpiresAtUtc] > [CreatedAtUtc]");
                tableBuilder.HasCheckConstraint(
                    "CK_RefreshTokens_RevokedAfterCreated",
                    "[RevokedAtUtc] IS NULL OR [RevokedAtUtc] >= [CreatedAtUtc]");
                tableBuilder.HasCheckConstraint(
                    "CK_RefreshTokens_Replacement_NotSelf",
                    "[ReplacedByTokenId] IS NULL OR [ReplacedByTokenId] <> [Id]");
            });

        builder.Property(token => token.TokenHash).HasColumnType("binary(32)").IsRequired();
        builder.Property(token => token.ExpiresAtUtc).HasColumnType("datetimeoffset(7)").IsRequired();
        builder.Property(token => token.RevokedAtUtc).HasColumnType("datetimeoffset(7)");
        builder.Property(token => token.CreatedByIp)
            .HasColumnType("varchar(45)")
            .IsUnicode(false);
        builder.Property(token => token.RevokedByIp)
            .HasColumnType("varchar(45)")
            .IsUnicode(false);
        builder.Property(token => token.RevocationReason).HasMaxLength(200);

        builder.HasIndex(token => token.TokenHash).IsUnique();
        builder.HasIndex(token => token.TokenFamilyId);
        builder.HasIndex(token => new { token.AppUserId, token.ExpiresAtUtc });
        builder.HasIndex(token => token.ReplacedByTokenId);

        builder.HasOne(token => token.AppUser)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(token => token.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(token => token.ReplacedByToken)
            .WithMany(token => token.PreviousTokens)
            .HasForeignKey(token => token.ReplacedByTokenId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
