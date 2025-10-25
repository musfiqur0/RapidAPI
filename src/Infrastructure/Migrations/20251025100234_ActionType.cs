using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId",
                table: "EmployeeAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAudits_ActionType_ActionTypeId",
                table: "GroupAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudits_ActionType_ActionTypeId",
                table: "JobPostAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentModeAudits_ActionType_ActionTypeId",
                table: "PaymentModeAudits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionType",
                table: "ActionType");

            migrationBuilder.RenameTable(
                name: "ActionType",
                newName: "ActionTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionTypes",
                table: "ActionTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAudits_ActionTypes_ActionTypeId",
                table: "EmployeeAudits",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAudits_ActionTypes_ActionTypeId",
                table: "GroupAudits",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudits_ActionTypes_ActionTypeId",
                table: "JobPostAudits",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentModeAudits_ActionTypes_ActionTypeId",
                table: "PaymentModeAudits",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAudits_ActionTypes_ActionTypeId",
                table: "EmployeeAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAudits_ActionTypes_ActionTypeId",
                table: "GroupAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudits_ActionTypes_ActionTypeId",
                table: "JobPostAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentModeAudits_ActionTypes_ActionTypeId",
                table: "PaymentModeAudits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionTypes",
                table: "ActionTypes");

            migrationBuilder.RenameTable(
                name: "ActionTypes",
                newName: "ActionType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionType",
                table: "ActionType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAudits_ActionType_ActionTypeId",
                table: "EmployeeAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAudits_ActionType_ActionTypeId",
                table: "GroupAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudits_ActionType_ActionTypeId",
                table: "JobPostAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentModeAudits_ActionType_ActionTypeId",
                table: "PaymentModeAudits",
                column: "ActionTypeId",
                principalTable: "ActionType",
                principalColumn: "Id");
        }
    }
}
