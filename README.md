# SheetYar

SheetYar is an English-only Flutter mobile application for Android and iOS. It helps people create and manage spreadsheet-style workbooks through guided workflows backed by an ASP.NET Core REST API.

## Repository Layout

- `apps/mobile`: Flutter application for Android and iOS.
- `apps/api/src/SheetYar.Domain`: Core domain types and rules.
- `apps/api/src/SheetYar.Application`: Application use-case boundary.
- `apps/api/src/SheetYar.Infrastructure`: Persistence and external-service boundary.
- `apps/api/src/SheetYar.Api`: ASP.NET Core API host.
- `tests/backend`: Backend automated tests.
- `apps/mobile/test`: Mobile automated tests.
- `docs`: Product, architecture, environment, licensing, and compatibility decisions.

## Prerequisites

- Flutter 3.47.5 with Dart 3.13.4.
- Android Studio and Android SDK Platform 36 for Android development.
- .NET SDK 8.0.302.
- SQL Server for API persistence.
- macOS and Xcode are required to build and sign the iOS application.

## Configuration

Use `env.example` as the local environment-variable reference. Keep real credentials and connection strings outside source control.
Authentication setup and endpoint behavior are documented in `docs/AUTHENTICATION.md`.

## Mobile Checks

```text
cd apps/mobile
flutter pub get
flutter analyze
flutter test
```

## Backend Checks

```text
dotnet restore
dotnet build
dotnet test --no-build
```

Run the API with:

```text
dotnet run --project apps/api/src/SheetYar.Api
```

The API exposes `GET /health`, the OpenAPI document at `/openapi/v1.json`, and the authentication endpoints documented in `docs/AUTHENTICATION.md`.
