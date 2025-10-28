using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowanceType_SourceType_OvertimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypeAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActionTypeId = table.Column<short>(type: "smallint", nullable: true),
                    ActionUserId = table.Column<long>(type: "bigint", nullable: false),
                    ActionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypeAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowanceTypeAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllowanceTypeAudits_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowanceTypeAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypeLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypeLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowanceTypeLocalizations_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowanceTypeLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeTypeAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OvertimeTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActionTypeId = table.Column<short>(type: "smallint", nullable: true),
                    ActionUserId = table.Column<long>(type: "bigint", nullable: false),
                    ActionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeTypeAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeTypeAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OvertimeTypeAudits_OvertimeTypes_OvertimeTypeId",
                        column: x => x.OvertimeTypeId,
                        principalTable: "OvertimeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeTypeAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OvertimeTypeLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    OvertimeTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeTypeLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeTypeLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeTypeLocalizations_OvertimeTypes_OvertimeTypeId",
                        column: x => x.OvertimeTypeId,
                        principalTable: "OvertimeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceTypeAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActionTypeId = table.Column<short>(type: "smallint", nullable: true),
                    ActionUserId = table.Column<long>(type: "bigint", nullable: false),
                    ActionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypeAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceTypeAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SourceTypeAudits_SourceTypes_SourceTypeId",
                        column: x => x.SourceTypeId,
                        principalTable: "SourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SourceTypeAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SourceTypeLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    SourceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypeLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceTypeLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SourceTypeLocalizations_SourceTypes_SourceTypeId",
                        column: x => x.SourceTypeId,
                        principalTable: "SourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypeAudits_ActionTypeId",
                table: "AllowanceTypeAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypeAudits_AllowanceTypeId",
                table: "AllowanceTypeAudits",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypeAudits_StatusTypeId",
                table: "AllowanceTypeAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypeLocalizations_AllowanceTypeId",
                table: "AllowanceTypeLocalizations",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceTypeLocalizations_LanguageId",
                table: "AllowanceTypeLocalizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeTypeAudits_ActionTypeId",
                table: "OvertimeTypeAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeTypeAudits_OvertimeTypeId",
                table: "OvertimeTypeAudits",
                column: "OvertimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeTypeAudits_StatusTypeId",
                table: "OvertimeTypeAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeTypeLocalizations_LanguageId",
                table: "OvertimeTypeLocalizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeTypeLocalizations_OvertimeTypeId",
                table: "OvertimeTypeLocalizations",
                column: "OvertimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeAudits_ActionTypeId",
                table: "SourceTypeAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeAudits_SourceTypeId",
                table: "SourceTypeAudits",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeAudits_StatusTypeId",
                table: "SourceTypeAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeLocalizations_LanguageId",
                table: "SourceTypeLocalizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeLocalizations_SourceTypeId",
                table: "SourceTypeLocalizations",
                column: "SourceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowanceTypeAudits");

            migrationBuilder.DropTable(
                name: "AllowanceTypeLocalizations");

            migrationBuilder.DropTable(
                name: "OvertimeTypeAudits");

            migrationBuilder.DropTable(
                name: "OvertimeTypeLocalizations");

            migrationBuilder.DropTable(
                name: "SourceTypeAudits");

            migrationBuilder.DropTable(
                name: "SourceTypeLocalizations");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "OvertimeTypes");

            migrationBuilder.DropTable(
                name: "SourceTypes");
        }
    }
}
