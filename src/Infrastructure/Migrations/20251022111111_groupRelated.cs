using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class groupRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActionAt",
                table: "GroupAudits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "ActionTypeId",
                table: "GroupAudits",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ActionUserId",
                table: "GroupAudits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "GroupAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "GroupAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "GroupAudits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "GroupAudits",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "GroupAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "GroupAudits",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapURL",
                table: "GroupAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "GroupAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "StatusTypeId",
                table: "GroupAudits",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAudits_ActionTypeId",
                table: "GroupAudits",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAudits_StatusTypeId",
                table: "GroupAudits",
                column: "StatusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAudits_ActionType_ActionTypeId",
                table: "GroupAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAudits_StatusTypes_StatusTypeId",
                table: "GroupAudits",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAudits_ActionType_ActionTypeId",
                table: "GroupAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAudits_StatusTypes_StatusTypeId",
                table: "GroupAudits");

            migrationBuilder.DropIndex(
                name: "IX_GroupAudits_ActionTypeId",
                table: "GroupAudits");

            migrationBuilder.DropIndex(
                name: "IX_GroupAudits_StatusTypeId",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "ActionAt",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "ActionTypeId",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "ActionUserId",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "MapURL",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "GroupAudits");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "GroupAudits");
        }
    }
}
