# SheetYar Architecture

## System Context

```text
Flutter Mobile App (Android and iOS)
              |
         HTTPS / REST
              |
       ASP.NET Core API
         |           |
      EF Core    XLSX Service
         |        ClosedXML
 SQL Server Express  |
                  .xlsx files
```

The mobile app owns presentation and interactive editing. The API owns authentication, authorization, business rules, persistence, versioning, and XLSX processing. The mobile app never connects directly to SQL Server or manipulates XLSX packages.

## Mobile Modules

- **App Shell:** Material 3 theme, `en-US`, LTR layout, routing, and error presentation.
- **Authentication:** Sign-in, token lifecycle, and secure token storage.
- **Dashboard:** Recent workbooks, templates, and primary actions.
- **Templates and Guided Builder:** Step-by-step workbook creation.
- **Spreadsheet Grid:** Mobile-first cell navigation and editing.
- **Formula Builder:** Creates formulas from a supported, versioned allowlist.
- **Charts:** Displays SheetYar chart definitions with `fl_chart`.
- **Versions:** Lists and restores workbook versions.
- **Import and Export:** Uploads and downloads XLSX files through the API.

## API Modules

- **Identity:** Authentication, authorization, sessions, and token renewal.
- **Workbooks:** Workbook metadata and ownership rules.
- **Sheets and Cells:** Worksheet structure, cell values, formulas, and styles.
- **Templates:** Template catalog and template-based creation.
- **Charts:** Chart definitions and source ranges.
- **Versions:** Immutable workbook snapshots and restore operations.
- **XLSX:** Validated import and export with ClosedXML.
- **Persistence:** EF Core mappings, transactions, and SQL Server access.

## Core Data Model

| Entity | Purpose | Main Relationships |
|---|---|---|
| User | Account identity and ownership | Owns workbooks and sessions |
| Session | Refresh-token lifecycle and revocation | Belongs to a user |
| Workbook | Spreadsheet document metadata | Owns worksheets, charts, and versions |
| Worksheet | Ordered sheet within a workbook | Owns cells |
| Cell | Typed value, optional formula, and style | Belongs to a worksheet |
| Chart | Chart type, source range, and display options | Belongs to a workbook or worksheet |
| WorkbookVersion | Immutable restorable snapshot | Belongs to a workbook |
| Template | Reusable guided workbook definition | Creates a new workbook |

Large binary XLSX payloads should not be stored in ordinary cell tables. Import and export should use bounded streams and temporary storage with cleanup rules.

## Boundaries

- Every API request must enforce workbook ownership or explicit access.
- Mobile DTOs must not expose database entities directly.
- Workbook mutations must be validated and versioned at the API boundary.
- Secrets and connection strings must remain outside source control.
- XLSX behavior must follow `docs/XLSX_COMPATIBILITY.md`.

## MVP Delivery Stages

1. **Foundation:** Repository structure, API health, mobile shell, configuration, and tests.
2. **Identity and Dashboard:** Authentication, secure sessions, workbook list, and creation.
3. **Builder and Grid:** Templates, Guided Builder, worksheet model, and cell editing.
4. **Formulas and Charts:** Supported formula allowlist and SheetYar chart definitions.
5. **Versions and XLSX:** Version history, restore, validated import, and export.
6. **Hardening:** Security review, compatibility fixtures, performance limits, and mobile release checks.
