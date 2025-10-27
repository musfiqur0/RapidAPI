using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeductionAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeductionId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IqamaNo = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DeductionTypeId = table.Column<int>(type: "int", nullable: false),
                    DeductionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_DeductionAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeductionAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeductionAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IqamaNo = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DeductionTypeId = table.Column<int>(type: "int", nullable: false),
                    DeductionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Draft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeductionLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    DeductionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeductionLocalizations_Deductions_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Deductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeductionLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeductionAudits_ActionTypeId",
                table: "DeductionAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionAudits_StatusTypeId",
                table: "DeductionAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionLocalizations_DeductionId",
                table: "DeductionLocalizations",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionLocalizations_LanguageId",
                table: "DeductionLocalizations",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeductionAudits");

            migrationBuilder.DropTable(
                name: "DeductionLocalizations");

            migrationBuilder.DropTable(
                name: "Deductions");
        }
    }
}
