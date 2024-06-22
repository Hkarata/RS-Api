using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSAllies.Test.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SelectedChoices",
                newName: "SelectedChoices",
                newSchema: "Test");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SelectedChoices",
                schema: "Test",
                newName: "SelectedChoices");
        }
    }
}
