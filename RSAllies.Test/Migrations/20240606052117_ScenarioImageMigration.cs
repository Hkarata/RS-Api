using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSAllies.Test.Migrations
{
    /// <inheritdoc />
    public partial class ScenarioImageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Test",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scenario",
                schema: "Test",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Test",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Scenario",
                schema: "Test",
                table: "Questions");
        }
    }
}
