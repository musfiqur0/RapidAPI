using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAudits_Employees_EmployeeId",
                table: "EmployeeAudits");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAudits_EmployeeId",
                table: "EmployeeAudits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAudits_EmployeeId",
                table: "EmployeeAudits",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAudits_Employees_EmployeeId",
                table: "EmployeeAudits",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
