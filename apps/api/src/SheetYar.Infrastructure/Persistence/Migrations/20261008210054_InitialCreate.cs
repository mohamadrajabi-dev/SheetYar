using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheetYar.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.CheckConstraint("CK_AppUsers_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_AppUsers_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_AppUsers_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorrelationId = table.Column<string>(type: "varchar(128)", unicode: false, nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    DetailsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.CheckConstraint("CK_AuditLogs_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_AuditLogs_DetailsJson_Valid", "[DetailsJson] IS NULL OR ISJSON([DetailsJson]) = 1");
                    table.CheckConstraint("CK_AuditLogs_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_AuditLogs_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.ForeignKey(
                        name: "FK_AuditLogs_AppUsers_ActorUserId",
                        column: x => x.ActorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    TokenFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RevokedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedByIp = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    RevokedByIp = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    RevocationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.CheckConstraint("CK_RefreshTokens_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_RefreshTokens_ExpiresAfterCreated", "[ExpiresAtUtc] > [CreatedAtUtc]");
                    table.CheckConstraint("CK_RefreshTokens_ExpiresAtUtc_UTC", "DATEPART(TZOFFSET, [ExpiresAtUtc]) = 0");
                    table.CheckConstraint("CK_RefreshTokens_Replacement_NotSelf", "[ReplacedByTokenId] IS NULL OR [ReplacedByTokenId] <> [Id]");
                    table.CheckConstraint("CK_RefreshTokens_RevokedAfterCreated", "[RevokedAtUtc] IS NULL OR [RevokedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_RefreshTokens_RevokedAtUtc_UTC", "[RevokedAtUtc] IS NULL OR DATEPART(TZOFFSET, [RevokedAtUtc]) = 0");
                    table.CheckConstraint("CK_RefreshTokens_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_RefreshTokens_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_RefreshTokens_ReplacedByTokenId",
                        column: x => x.ReplacedByTokenId,
                        principalTable: "RefreshTokens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkbookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoragePath = table.Column<string>(type: "varchar(512)", unicode: false, nullable: false),
                    ContentType = table.Column<string>(type: "varchar(100)", unicode: false, nullable: false),
                    Extension = table.Column<string>(type: "varchar(16)", unicode: false, nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    Sha256 = table.Column<string>(type: "varchar(64)", unicode: false, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAssets", x => x.Id);
                    table.CheckConstraint("CK_FileAssets_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_FileAssets_Kind_Valid", "[Kind] IN (1, 2, 3, 4)");
                    table.CheckConstraint("CK_FileAssets_Sha256_Valid", "LEN([Sha256]) = 64 AND [Sha256] COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9A-Fa-f]%'");
                    table.CheckConstraint("CK_FileAssets_SizeBytes_NonNegative", "[SizeBytes] >= 0");
                    table.CheckConstraint("CK_FileAssets_StoragePath_Relative", "LEN(LTRIM(RTRIM([StoragePath]))) > 0 AND [StoragePath] NOT LIKE '%..%' AND CHARINDEX(':', [StoragePath]) = 0 AND CHARINDEX('\\', [StoragePath]) = 0 AND LEFT([StoragePath], 1) <> '/'");
                    table.CheckConstraint("CK_FileAssets_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_FileAssets_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.ForeignKey(
                        name: "FK_FileAssets_AppUsers_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SourceFileAssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.CheckConstraint("CK_Templates_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_Templates_SortOrder_NonNegative", "[SortOrder] >= 0");
                    table.CheckConstraint("CK_Templates_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_Templates_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.ForeignKey(
                        name: "FK_Templates_FileAssets_SourceFileAssetId",
                        column: x => x.SourceFileAssetId,
                        principalTable: "FileAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workbooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workbooks", x => x.Id);
                    table.CheckConstraint("CK_Workbooks_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_Workbooks_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_Workbooks_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.ForeignKey(
                        name: "FK_Workbooks_AppUsers_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workbooks_Templates_SourceTemplateId",
                        column: x => x.SourceTemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WorkbookVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkbookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SnapshotFileAssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChangeSummary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkbookVersions", x => x.Id);
                    table.CheckConstraint("CK_WorkbookVersions_CreatedAtUtc_UTC", "DATEPART(TZOFFSET, [CreatedAtUtc]) = 0");
                    table.CheckConstraint("CK_WorkbookVersions_UpdatedAtUtc_NotBeforeCreated", "[UpdatedAtUtc] >= [CreatedAtUtc]");
                    table.CheckConstraint("CK_WorkbookVersions_UpdatedAtUtc_UTC", "DATEPART(TZOFFSET, [UpdatedAtUtc]) = 0");
                    table.CheckConstraint("CK_WorkbookVersions_VersionNumber_Positive", "[VersionNumber] > 0");
                    table.ForeignKey(
                        name: "FK_WorkbookVersions_AppUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkbookVersions_FileAssets_SnapshotFileAssetId",
                        column: x => x.SnapshotFileAssetId,
                        principalTable: "FileAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkbookVersions_Workbooks_WorkbookId",
                        column: x => x.WorkbookId,
                        principalTable: "Workbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_IsActive_CreatedAtUtc",
                table: "AppUsers",
                columns: new[] { "IsActive", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_NormalizedEmail",
                table: "AppUsers",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActorUserId_CreatedAtUtc",
                table: "AuditLogs",
                columns: new[] { "ActorUserId", "CreatedAtUtc" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedAtUtc",
                table: "AuditLogs",
                column: "CreatedAtUtc",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityType_EntityId_CreatedAtUtc",
                table: "AuditLogs",
                columns: new[] { "EntityType", "EntityId", "CreatedAtUtc" },
                descending: new[] { false, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_FileAssets_OwnerUserId_CreatedAtUtc",
                table: "FileAssets",
                columns: new[] { "OwnerUserId", "CreatedAtUtc" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_FileAssets_Sha256",
                table: "FileAssets",
                column: "Sha256");

            migrationBuilder.CreateIndex(
                name: "IX_FileAssets_StoragePath",
                table: "FileAssets",
                column: "StoragePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAssets_WorkbookId_CreatedAtUtc",
                table: "FileAssets",
                columns: new[] { "WorkbookId", "CreatedAtUtc" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId_ExpiresAtUtc",
                table: "RefreshTokens",
                columns: new[] { "AppUserId", "ExpiresAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ReplacedByTokenId",
                table: "RefreshTokens",
                column: "ReplacedByTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenFamilyId",
                table: "RefreshTokens",
                column: "TokenFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Code",
                table: "Templates",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_IsActive_SortOrder",
                table: "Templates",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_SourceFileAssetId",
                table: "Templates",
                column: "SourceFileAssetId",
                unique: true,
                filter: "[SourceFileAssetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Workbooks_OwnerUserId_UpdatedAtUtc",
                table: "Workbooks",
                columns: new[] { "OwnerUserId", "UpdatedAtUtc" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Workbooks_SourceTemplateId",
                table: "Workbooks",
                column: "SourceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkbookVersions_CreatedByUserId",
                table: "WorkbookVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkbookVersions_SnapshotFileAssetId",
                table: "WorkbookVersions",
                column: "SnapshotFileAssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkbookVersions_WorkbookId_CreatedAtUtc",
                table: "WorkbookVersions",
                columns: new[] { "WorkbookId", "CreatedAtUtc" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_WorkbookVersions_WorkbookId_VersionNumber",
                table: "WorkbookVersions",
                columns: new[] { "WorkbookId", "VersionNumber" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAssets_Workbooks_WorkbookId",
                table: "FileAssets",
                column: "WorkbookId",
                principalTable: "Workbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileAssets_AppUsers_OwnerUserId",
                table: "FileAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Workbooks_AppUsers_OwnerUserId",
                table: "Workbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAssets_Workbooks_WorkbookId",
                table: "FileAssets");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "WorkbookVersions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Workbooks");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "FileAssets");
        }
    }
}
