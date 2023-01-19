using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjectResourceDailyTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DailyTasks",
                newName: "ProjectResourceDailyTasks");

            migrationBuilder.RenameIndex(
                name: "IX_DailyTasks_ProjectResourceId",
                table: "ProjectResourceDailyTasks",
                newName: "IX_ProjectResourceDailyTasks_ProjectResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ProjectResourceDailyTasks",
                newName: "DailyTasks");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectResourceDailyTasks_ProjectResourceId",
                table: "DailyTasks",
                newName: "IX_DailyTasks_ProjectResourceId");
        }
    }
}
