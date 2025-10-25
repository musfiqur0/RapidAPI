using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobPostUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StatusId1",
                table: "JobPosts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_StatusId1",
                table: "JobPosts",
                column: "StatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Status_StatusId1",
                table: "JobPosts",
                column: "StatusId1",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Status_StatusId1",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_JobPosts_StatusId1",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "JobPosts");
        }
    }
}
