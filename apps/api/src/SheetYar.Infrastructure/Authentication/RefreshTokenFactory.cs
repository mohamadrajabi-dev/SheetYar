using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SheetYar.Domain.Authentication;

namespace SheetYar.Infrastructure.Authentication;

internal sealed record GeneratedRefreshToken(string Value, RefreshToken Entity);

internal interface IRefreshTokenFactory
{
    GeneratedRefreshToken Create(
        Guid userId,
        Guid familyId,
        DateTimeOffset expiresAtUtc,
        string? clientIpAddress);

    byte[] Hash(string value);
}

internal sealed class RefreshTokenFactory : IRefreshTokenFactory
{
    private const int TokenByteLength = 64;

    public GeneratedRefreshToken Create(
        Guid userId,
        Guid familyId,
        DateTimeOffset expiresAtUtc,
        string? clientIpAddress)
    {
        var value = Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(TokenByteLength));
        var entity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            AppUserId = userId,
            TokenHash = Hash(value),
            TokenFamilyId = familyId,
            ExpiresAtUtc = expiresAtUtc.ToUniversalTime(),
            CreatedByIp = NormalizeIpAddress(clientIpAddress),
        };

        return new GeneratedRefreshToken(value, entity);
    }

    public byte[] Hash(string value) => SHA256.HashData(Encoding.UTF8.GetBytes(value));

    private static string? NormalizeIpAddress(string? clientIpAddress) =>
        string.IsNullOrWhiteSpace(clientIpAddress)
            ? null
            : clientIpAddress.Trim()[..Math.Min(clientIpAddress.Trim().Length, 45)];
}
