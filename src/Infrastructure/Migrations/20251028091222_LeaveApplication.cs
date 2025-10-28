using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaveApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTypesId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplicationAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypesId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_LeaveApplicationAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationAudits_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveApplicationAudits_LeaveApplications_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "LeaveApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationAudits_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplicationLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplicationLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationLocalizations_LeaveApplications_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "LeaveApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationAudits_ActionTypeId",
                table: "LeaveApplicationAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationAudits_LeaveApplicationId",
                table: "LeaveApplicationAudits",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationAudits_StatusTypeId",
                table: "LeaveApplicationAudits",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationLocalizations_LanguageId",
                table: "LeaveApplicationLocalizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationLocalizations_LeaveApplicationId",
                table: "LeaveApplicationLocalizations",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_StatusId",
                table: "LeaveApplications",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveApplicationAudits");

            migrationBuilder.DropTable(
                name: "LeaveApplicationLocalizations");

            migrationBuilder.DropTable(
                name: "LeaveApplications");
        }
    }
}
