using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSAllies.Venues.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "Districts",
                newSchema: "Venues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Districts",
                schema: "Venues",
                newName: "Districts");
        }
    }
}
