# Database Operations

## Database Platform

SheetYar uses Entity Framework Core with SQL Server. The local Windows development environment uses SQL Server 2022 Developer edition. Microsoft licenses Developer edition for development and testing only, so the production edition, topology, capacity, and license must be selected separately before deployment.

Official references:

- [SQL Server 2022 editions and supported features](https://learn.microsoft.com/en-us/sql/sql-server/editions-and-components-of-sql-server-2022)
- [SQL Server downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Apply EF Core migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying)
- [ASP.NET Core environment-variable configuration](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/#environment-variables)

## Configuration

The API reads the database connection only from `ConnectionStrings:SheetYar`. Supply it outside the repository with the `ConnectionStrings__SheetYar` environment variable, a local secret store, or the deployment platform's secret manager. Never commit a real connection string, password, token, certificate, or production host name.

Local Windows Authentication example for the default local instance:

```powershell
$env:ConnectionStrings__SheetYar = 'Server=localhost;Database=SheetYar;Integrated Security=True;Encrypt=True;TrustServerCertificate=True'
```

`TrustServerCertificate=True` is acceptable only for a trusted local development instance. Production must use certificate validation and deployment-managed credentials. The migration identity should have schema-change permission; the normal API identity should receive only the permissions required at runtime.

## Migrations

Run EF Core commands from the repository root. The migration project is Infrastructure and the startup project is Api.

Restore the repository-local EF Core tool after a fresh clone:

```powershell
dotnet tool restore
```

List migrations:

```powershell
dotnet tool run dotnet-ef migrations list --project apps/api/src/SheetYar.Infrastructure --startup-project apps/api/src/SheetYar.Api --context SheetYarDbContext
```

Apply all pending migrations to the configured database:

```powershell
dotnet tool run dotnet-ef database update --project apps/api/src/SheetYar.Infrastructure --startup-project apps/api/src/SheetYar.Api --context SheetYarDbContext
```

Check that the model matches the latest migration:

```powershell
dotnet tool run dotnet-ef migrations has-pending-model-changes --project apps/api/src/SheetYar.Infrastructure --startup-project apps/api/src/SheetYar.Api --context SheetYarDbContext
```

Generate a reviewable idempotent deployment script:

```powershell
dotnet tool run dotnet-ef migrations script --idempotent --project apps/api/src/SheetYar.Infrastructure --startup-project apps/api/src/SheetYar.Api --context SheetYarDbContext --output artifacts/SheetYar.Database.sql
```

Use `dotnet tool run dotnet-ef database update` for local development. For production, review and test an idempotent script or migration bundle and run it as a controlled deployment step. Do not call `EnsureCreated` before migrations because it bypasses migration history.

## Idempotent Seed

Automatic database initialization is disabled by default. In development or test, enable it for one API startup to apply pending migrations and run the idempotent seed:

```powershell
$env:Database__InitializeOnStartup = 'true'
dotnet run --project apps/api/src/SheetYar.Api
Remove-Item Env:Database__InitializeOnStartup
```

Stop the API after initialization and remove the temporary environment variable. Do not save this flag as `true` in a committed settings file.

Seed operations must use stable business keys, query before inserting, and remain safe when invoked more than once. To verify a seed change:

1. Apply migrations to an empty test database.
2. Start the API once with `Database__InitializeOnStartup=true` and record the seeded row keys and counts.
3. Stop and start it again with the same flag and database, without clearing any data.
4. Confirm that the same keys and counts remain and no duplicate rows were created.

Seed data must not contain credentials, tokens, personal data, environment-specific URLs, or secrets.

In production, keep `Database__InitializeOnStartup` disabled on normal API instances. Apply the reviewed idempotent migration script first, then run the initializer once as a controlled deployment action before regular instances start or scale out.

## XLSX File Storage

XLSX binaries must not be stored in SQL Server. `FileAsset` stores metadata and a relative, application-controlled storage path only. Recommended metadata includes the owning record, original display name, generated storage name, media type, byte length, checksum, and UTC timestamps.

Configure the storage root outside the repository and outside the API web root:

```powershell
$env:Storage__RootPath = 'E:\SheetYarData\files'
```

The API must:

- Generate opaque storage names, such as a `Guid`, instead of using an uploaded file name as a path.
- Normalize every resolved path and verify that it remains below the configured storage root.
- Reject path traversal, absolute client paths, unsupported extensions, and oversized uploads.
- Enforce asset ownership and authorization before every read, write, export, or delete.
- Use bounded streams, checksum validation, atomic replacement, and cleanup for abandoned temporary files.
- Store only relative paths in the database so that the storage root can change by environment.
- Validate that version snapshots use `VersionSnapshot`, template sources use `TemplateSource`, and asset ownership matches the related workbook before saving metadata.

The file store requires an independent backup, restore, retention, and deletion policy coordinated with SQL Server backups; a database backup alone is not a complete SheetYar backup.
