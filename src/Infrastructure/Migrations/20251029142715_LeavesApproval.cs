using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeavesApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeavesApprovalAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeavesApprovalId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Employee = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    HardCopy = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
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
                    table.PrimaryKey("PK_LeavesApprovalAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavesApprovalAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeavesApprovalAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeavesApprovalAudits_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeavesApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Employee = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    HardCopy = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavesApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavesApprovals_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeavesApprovalLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeavesApprovalId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavesApprovalLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavesApprovalLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeavesApprovalLocalizations_LeavesApprovals_LeavesApprovalId",
                        column: x => x.LeavesApprovalId,
                        principalTable: "LeavesApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovalAudits_ActionTypeId",
                table: "LeavesApprovalAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovalAudits_StatusId",
                table: "LeavesApprovalAudits",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovalAudits_StatusTypeId",
                table: "LeavesApprovalAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovalLocalizations_LanguageId",
                table: "LeavesApprovalLocalizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovalLocalizations_LeavesApprovalId",
                table: "LeavesApprovalLocalizations",
                column: "LeavesApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesApprovals_StatusId",
                table: "LeavesApprovals",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeavesApprovalAudits");

            migrationBuilder.DropTable(
                name: "LeavesApprovalLocalizations");

            migrationBuilder.DropTable(
                name: "LeavesApprovals");
        }
    }
}
