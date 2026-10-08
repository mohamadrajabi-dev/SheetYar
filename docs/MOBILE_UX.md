# SheetYar Mobile UX

## UX Principles

- Design for users who do not know Excel terminology or formula syntax.
- Use English only, left-to-right layout, and `en-US` formatting.
- Use Material 3 patterns and touch targets appropriate for phones and tablets.
- Prefer guided choices, previews, and plain-language labels over spreadsheet jargon.
- Make destructive actions explicit and recoverable through version history where possible.
- Keep Web and desktop interactions outside the product scope.

## Primary Navigation

The main navigation contains Dashboard, Templates, Workbooks, and Account. Editing opens as a focused workbook flow with access to Sheets, Formula Builder, Charts, Versions, and Export.

## Core User Flows

### Create a Workbook

1. Sign in and open the Dashboard.
2. Choose a template, Guided Builder, or blank workbook.
3. Enter a title and answer the minimum required setup questions.
4. Review the generated structure and open the Spreadsheet Grid.

### Edit Data and Formulas

1. Select a cell in the grid.
2. Enter a value or open Formula Builder.
3. Choose a supported operation and source cells.
4. Preview the result, confirm, and save.

### Create a Chart

1. Select a data range.
2. Choose a supported chart type.
3. Preview labels and values.
4. Save the chart to the workbook.

### Import XLSX

1. Select an `.xlsx` file.
2. Upload it for server-side validation.
3. Review compatibility warnings and the import summary.
4. Confirm creation of a new SheetYar workbook.

### Restore or Export

1. Open Version History to preview and restore a snapshot, or choose Export.
2. Review any XLSX compatibility warnings.
3. Confirm the action and show a clear success or failure message.

## Spreadsheet Grid Behavior

- Keep row and column headers visible while scrolling.
- Support clear selected, editing, loading, invalid, and read-only states.
- Use a dedicated editor for long text, dates, numbers, and formulas.
- Provide undo for the current editing session where feasible.
- Avoid hidden gestures for essential actions; pair gestures with visible controls.
- Apply practical workbook, worksheet, row, column, and cell limits defined by performance testing.

## Feedback and Accessibility

- Use plain English validation messages with a direct recovery action.
- Never rely on color alone to communicate state.
- Support dynamic text sizing, screen-reader labels, focus order, and sufficient contrast.
- Confirm long-running import and export operations with progress and cancellation when safe.
- Preserve unsaved edits during temporary network failures and clearly show sync status.
