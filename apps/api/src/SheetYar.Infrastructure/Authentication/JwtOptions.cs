namespace SheetYar.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SigningKey { get; set; } = string.Empty;

    public int AccessTokenMinutes { get; set; } = 10;

    public int RefreshTokenDays { get; set; } = 30;

    public bool HasValidSigningKey()
    {
        if (string.IsNullOrWhiteSpace(SigningKey))
        {
            return false;
        }

        try
        {
            return Convert.FromBase64String(SigningKey).Length >= 32;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public byte[] GetSigningKeyBytes()
    {
        if (!HasValidSigningKey())
        {
            throw new InvalidOperationException("JWT signing key must be valid Base64 containing at least 32 bytes.");
        }

        return Convert.FromBase64String(SigningKey);
    }
}
