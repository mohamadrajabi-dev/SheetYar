using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SheetYar.Domain.Users;

namespace SheetYar.Infrastructure.Authentication;

internal sealed class LoginTimingProtector
{
    private readonly AppUser dummyUser = new();
    private readonly PasswordHasher<AppUser> passwordHasher;
    private readonly string dummyPasswordHash;

    public LoginTimingProtector(IOptions<PasswordHasherOptions> options)
    {
        passwordHasher = new PasswordHasher<AppUser>(options);
        dummyPasswordHash = passwordHasher.HashPassword(
            dummyUser,
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)));
    }

    public void VerifyUnknownUserPassword(string suppliedPassword) =>
        _ = passwordHasher.VerifyHashedPassword(
            dummyUser,
            dummyPasswordHash,
            suppliedPassword);
}
