# Authentication

## Security Model

SheetYar uses ASP.NET Core Identity for users and password hashing. The API issues an HMAC-SHA256 JWT access token with a default lifetime of 10 minutes and an opaque 64-byte refresh token with a default absolute lifetime of 30 days.

Only a SHA-256 hash of each refresh token is stored in SQL Server. Refreshing rotates the token without extending the token family's absolute expiry. Reusing a revoked or replaced token revokes the entire family and changes the user's security stamp, which invalidates previously issued access tokens. `revoke-all` applies the same access-token invalidation to every session. Five failed password checks lock the account for 15 minutes.

The mobile application keeps access tokens in memory. It persists only the refresh token through `flutter_secure_storage`, using device-bound Android storage and the iOS Keychain. Android application backup is disabled so encrypted credentials are not restored onto a different device.

## Endpoints

| Method | Route | Authorization | Result |
|---|---|---|---|
| `POST` | `/api/v1/auth/register` | Anonymous | Creates an account and returns a session |
| `POST` | `/api/v1/auth/login` | Anonymous | Verifies credentials and returns a session |
| `POST` | `/api/v1/auth/refresh` | Anonymous | Rotates the refresh token and returns a new session |
| `POST` | `/api/v1/auth/logout` | Bearer | Revokes the submitted refresh token |
| `POST` | `/api/v1/auth/revoke-all` | Bearer | Revokes all refresh tokens and existing access tokens |
| `GET` | `/api/v1/auth/me` | Bearer | Returns the current user profile |

Authentication endpoints use IP-partitioned fixed-window rate limits. Authentication failures use the shared JSON Problem Details contract and do not disclose whether an email address exists.

## Required Configuration

Generate a random signing key outside the repository:

```powershell
[Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(64))
```

Set the output as `Jwt__SigningKey` in a local secret store or deployment secret manager. The decoded key must contain at least 32 bytes. Configure `ConnectionStrings__SheetYar` separately as described in `docs/DATABASE.md`. Never commit either value.

Production traffic must use HTTPS. If the API is deployed behind a reverse proxy, configure trusted forwarded headers at the deployment boundary so IP-based rate limits receive the real client address.

## Verification

```text
dotnet build SheetYar.sln --no-restore
dotnet test SheetYar.sln --no-build --no-restore
cd apps/mobile
flutter pub get
flutter analyze
flutter test
```

Run the SQL Server concurrency test against a local development instance with:

```powershell
$env:SHEETYAR_TEST_SQLSERVER = 'Server=localhost;Integrated Security=True;Encrypt=False;TrustServerCertificate=True'
dotnet test SheetYar.sln --filter "Category=SqlServer"
Remove-Item Env:SHEETYAR_TEST_SQLSERVER
```

The test creates an isolated random database, applies every migration, verifies concurrent refresh-token reuse, and deletes the database during cleanup.
