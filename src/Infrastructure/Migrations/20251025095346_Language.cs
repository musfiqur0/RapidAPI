using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Language : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLocalizations_Language_LanguageId",
                table: "EmployeeLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupLocalizations_Language_LanguageId",
                table: "GroupLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostLocalizations_Language_LanguageId",
                table: "JobPostLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentModeLocalizations_Language_LanguageId",
                table: "PaymentModeLocalizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLocalizations_Languages_LanguageId",
                table: "EmployeeLocalizations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLocalizations_Languages_LanguageId",
                table: "GroupLocalizations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostLocalizations_Languages_LanguageId",
                table: "JobPostLocalizations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentModeLocalizations_Languages_LanguageId",
                table: "PaymentModeLocalizations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLocalizations_Languages_LanguageId",
                table: "EmployeeLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupLocalizations_Languages_LanguageId",
                table: "GroupLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostLocalizations_Languages_LanguageId",
                table: "JobPostLocalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentModeLocalizations_Languages_LanguageId",
                table: "PaymentModeLocalizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLocalizations_Language_LanguageId",
                table: "EmployeeLocalizations",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLocalizations_Language_LanguageId",
                table: "GroupLocalizations",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostLocalizations_Language_LanguageId",
                table: "JobPostLocalizations",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentModeLocalizations_Language_LanguageId",
                table: "PaymentModeLocalizations",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
