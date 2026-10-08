# XLSX Compatibility

## Compatibility Contract

SheetYar supports a deliberate XLSX subset for its MVP. It does not claim full Microsoft Excel compatibility. A ClosedXML capability is not automatically a SheetYar capability; a feature moves to **Supported** only after import, edit, export, and round-trip fixtures pass for the pinned dependency version.

## Supported

The MVP targets reliable behavior for:

- `.xlsx` files only.
- Workbooks with one or more ordinary worksheets.
- Blank, text, numeric, Boolean, date, and time cell values.
- Worksheet names, order, visibility, and basic dimensions.
- Common font, fill, border, alignment, and number-format properties.
- Basic merged ranges within tested size limits.
- Formulas created by SheetYar's versioned formula allowlist.
- Import and export of SheetYar-created workbooks within documented size limits.
- Workbook data used by SheetYar charts, without promising native Excel chart fidelity.

## Degraded

These features may be preserved, simplified, converted, or reported with a warning:

- Formulas outside the SheetYar allowlist; the expression may be retained without calculation.
- Complex or locale-specific number formats.
- Conditional formatting, data validation, named ranges, tables, and filters.
- Embedded images, drawings, native Excel charts, comments, and rich text.
- Hidden or protected content that can be read but not safely edited.
- Very large or highly styled workbooks that exceed mobile or server limits.
- Layout-sensitive settings such as print areas, page breaks, exact row heights, and exact column widths.

Degraded content must never be silently represented as fully preserved. Import and export summaries must identify known losses or simplifications.

## Unsupported

The MVP rejects, removes only with explicit confirmation, or leaves untouched without editing:

- Legacy `.xls`, binary `.xlsb`, and macro-enabled `.xlsm` files.
- VBA, macros, ActiveX, form controls, OLE objects, add-ins, and digital signatures.
- Power Query, external data connections, linked workbooks, and the Excel Data Model.
- Pivot tables, pivot charts, slicers, timelines, and cube formulas.
- Password-encrypted workbooks and unsupported protection schemes.
- Data tables, advanced array or dynamic-array behavior outside the tested formula subset.
- Full Excel calculation-engine, rendering, and pixel-perfect layout compatibility.

## Import Rules

1. Validate extension, MIME signature, archive safety, size, and worksheet limits before processing.
2. Import into a new workbook; never overwrite an existing workbook implicitly.
3. Produce a compatibility summary containing supported, degraded, skipped, and rejected items.
4. Keep the original file available for safe recovery according to the retention policy.
5. Fail safely when corruption, encryption, or unsupported executable content is detected.

## Export Rules

1. Export only the supported SheetYar model to `.xlsx`.
2. Use invariant internal values and explicit `en-US` display formats.
3. Recalculate only the tested formula subset; allow Excel to recalculate other preserved formulas when safe.
4. Validate the generated package and reopen it in an automated round-trip test.
5. Warn before export when the workbook contains degraded features.

## Verification Gate

Maintain representative XLSX fixtures for every supported feature and regression case. Test create, import, edit, export, reopen, values, formulas, styles, warnings, and corruption handling. Update this document whenever a fixture changes a compatibility classification.

## Reference Basis

- ClosedXML documentation: https://docs.closedxml.io/
- ClosedXML source and feature documentation: https://github.com/ClosedXML/ClosedXML
