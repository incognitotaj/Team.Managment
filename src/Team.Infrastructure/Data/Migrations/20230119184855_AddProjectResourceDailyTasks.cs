using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectResourceDailyTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ProjectResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTasks_ProjectResources",
                        column: x => x.ProjectResourceId,
                        principalTable: "ProjectResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_ProjectResourceId",
                table: "DailyTasks",
                column: "ProjectResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTasks");
        }
    }
}
