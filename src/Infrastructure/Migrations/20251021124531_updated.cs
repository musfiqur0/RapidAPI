using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId1",
                table: "EmployeeAudits");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAudits_ActionTypeId1",
                table: "EmployeeAudits");

            migrationBuilder.DropColumn(
                name: "ActionTypeId1",
                table: "EmployeeAudits");

            migrationBuilder.AlterColumn<short>(
                name: "ActionTypeId",
                table: "EmployeeAudits",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAudits_ActionTypeId",
                table: "EmployeeAudits",
                column: "ActionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId",
                table: "EmployeeAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId",
                table: "EmployeeAudits");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAudits_ActionTypeId",
                table: "EmployeeAudits");

            migrationBuilder.AlterColumn<int>(
                name: "ActionTypeId",
                table: "EmployeeAudits",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ActionTypeId1",
                table: "EmployeeAudits",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAudits_ActionTypeId1",
                table: "EmployeeAudits",
                column: "ActionTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId1",
                table: "EmployeeAudits",
                column: "ActionTypeId1",
                principalTable: "ActionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
