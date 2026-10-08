# SheetYar Repository Instructions

These instructions apply to the entire repository.

## Required Workflow

1. Read `AGENTS.md`, `docs/PROJECT.md`, `docs/DECISIONS.md`, `docs/LICENSES.md`, and all task-relevant files before making changes.
2. Change only what the user requested.
3. Do not perform unrelated rewrites, refactors, formatting, or cleanup.
4. Never commit secrets, credentials, tokens, connection strings, signing files, or private user data.
5. Do not add or update a dependency until its license has been verified and recorded in `docs/LICENSES.md`.
6. Run the tests and checks relevant to the changed area.
7. Do not create a Git commit or push changes unless the user explicitly requests it.
8. Write the final report to the user in Persian and keep it to a maximum of five lines.

## Product Boundaries

- SheetYar is a Flutter mobile application for Android and iOS only.
- Do not create, enable, or maintain Web, Windows, macOS, or Linux application targets.
- Use Material 3.
- All user-facing UI text, errors, notifications, templates, generated content, and exported output must be English, left-to-right, and formatted for `en-US`.
- Do not add Persian text to the product or repository.
- Source code, filenames, classes, variables, APIs, database objects, tests, and documentation must be written in English.

## Approved Technology Stack

### Mobile

- Flutter and Dart
- Material 3
- Riverpod
- `go_router`
- Dio
- `flutter_secure_storage`
- `two_dimensional_scrollables` with a custom Spreadsheet Grid
- `fl_chart`

### Backend and Data

- ASP.NET Core and C#
- Entity Framework Core
- SQL Server Express
- ClosedXML for XLSX import and export

## Dependency Policy

- Use only free dependencies licensed under MIT, BSD-2-Clause, BSD-3-Clause, or Apache-2.0.
- Verify each dependency against an authoritative source before installation.
- Record its exact version, license, source URL, purpose, and verification date in `docs/LICENSES.md` before adding it.
- Do not use Syncfusion, DevExpress, Telerik, Aspose, Infragistics, SpreadJS, `@univerjs-pro`, paid cloud services, subscription services, or any dependency that requires a commercial license.

## MVP Scope

- Authentication
- Dashboard
- Templates
- Guided Builder
- Spreadsheet Grid Editor
- Formula Builder
- Charts
- Version History
- XLSX import and export

## Outside the MVP

- VBA or macros
- Power Query
- Pivot tables
- Real-time collaboration
- Full Microsoft Excel compatibility
