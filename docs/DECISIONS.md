# SheetYar Architecture Decisions

This file records accepted product and engineering decisions. Update it when a future decision changes architecture, scope, licensing, platforms, or user-visible behavior.

## D-001: Mobile-Only Product

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Build SheetYar with Flutter for Android and iOS only.
- **Consequence:** Web and desktop targets must not be created, enabled, or maintained.

## D-002: English-Only Experience

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Use English, left-to-right layout, and the `en-US` locale for all user-facing content and output.
- **Consequence:** Persian and other localized product content are outside the current scope.

## D-003: Flutter Application Stack

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Use Material 3, Riverpod, `go_router`, Dio, and `flutter_secure_storage` for the mobile application.
- **Consequence:** Alternative frameworks require an explicit new decision before adoption.

## D-004: Custom Spreadsheet Grid

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Build a custom Spreadsheet Grid on `two_dimensional_scrollables` instead of using a commercial spreadsheet component.
- **Consequence:** Grid behaviors must be implemented and tested within SheetYar's supported MVP scope.

## D-005: Charting

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Use `fl_chart` for supported mobile chart experiences.
- **Consequence:** Chart functionality must remain within the library's verified free-license terms and the MVP scope.

## D-006: Backend and Workbook Processing

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Use ASP.NET Core, C#, Entity Framework Core, SQL Server Express, and ClosedXML.
- **Consequence:** XLSX processing belongs to the backend; the mobile application consumes it through APIs.

## D-007: Dependency Licensing

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Permit only free dependencies using MIT, BSD-2-Clause, BSD-3-Clause, or Apache-2.0 licenses.
- **Consequence:** Every dependency must be verified and entered in `docs/LICENSES.md` before installation. Commercial, subscription-based, or restricted-license components are prohibited.

## D-008: MVP Boundary

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** The MVP includes Authentication, Dashboard, Templates, Guided Builder, Spreadsheet Grid Editor, Formula Builder, Charts, Version History, and XLSX import/export.
- **Consequence:** VBA, macros, Power Query, pivot tables, real-time collaboration, and complete Excel compatibility are excluded.

## D-009: Change and Delivery Discipline

- **Status:** Accepted
- **Date:** 2026-10-07
- **Decision:** Keep changes request-scoped, protect secrets, run relevant tests, and commit only upon explicit request.
- **Consequence:** Codex final reports to the user must be in Persian and no longer than five lines.
