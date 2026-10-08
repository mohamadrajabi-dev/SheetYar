# SheetYar Project

## Product Summary

SheetYar is a mobile application that enables people without Microsoft Excel knowledge to create, edit, visualize, import, and export spreadsheet-based workbooks through guided workflows.

## Supported Platforms

- Android
- iOS

Web and desktop applications are explicitly excluded. The repository must not introduce or maintain Flutter targets for Web, Windows, macOS, or Linux.

## User Experience Requirements

- Use Material 3 throughout the mobile application.
- Display all user-facing content in English.
- Use left-to-right layout direction.
- Use the `en-US` locale for dates, numbers, validation messages, templates, and generated output.
- Keep spreadsheet creation understandable for users who do not know Excel terminology or formulas.
- Do not include Persian text in the application, generated workbooks, documentation, or repository content.

## Architecture

### Mobile Application

- Flutter and Dart
- Riverpod for application state and dependency management
- `go_router` for navigation
- Dio for HTTP communication
- `flutter_secure_storage` for sensitive client-side values
- `two_dimensional_scrollables` as the foundation for a custom Spreadsheet Grid
- `fl_chart` for charts

### Backend

- ASP.NET Core and C#
- Entity Framework Core for persistence
- SQL Server Express as the database
- ClosedXML for XLSX import and export

## MVP Capabilities

1. **Authentication**: Secure account access and session handling.
2. **Dashboard**: Entry point for recent workbooks, templates, and primary actions.
3. **Templates**: Ready-to-use spreadsheet structures for common scenarios.
4. **Guided Builder**: Step-by-step workbook creation without requiring spreadsheet expertise.
5. **Spreadsheet Grid Editor**: Custom mobile-first cell editing and navigation.
6. **Formula Builder**: Guided creation of supported formulas without requiring users to type formula syntax.
7. **Charts**: Create visualizations from selected workbook data.
8. **Version History**: Preserve and restore meaningful workbook versions.
9. **XLSX Import and Export**: Exchange workbook files through the backend using ClosedXML.

## Explicit Non-Goals for the MVP

- VBA and macros
- Power Query
- Pivot tables
- Real-time collaboration
- Complete behavioral or rendering compatibility with Microsoft Excel

## Engineering Constraints

- Use only dependencies permitted by `AGENTS.md` and recorded in `docs/LICENSES.md`.
- Never place secrets in source control.
- Keep changes scoped to the active request.
- Add and run tests appropriate to each implementation change.
- Do not commit or push unless explicitly requested by the user.
