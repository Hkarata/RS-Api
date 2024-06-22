using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSAllies.Test.Migrations
{
    /// <inheritdoc />
    public partial class SelectedChoiceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Responses",
                schema: "Test",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Test",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "ChoiceId",
                schema: "Test",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "IsChoiceCorrect",
                schema: "Test",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                schema: "Test",
                table: "Responses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responses",
                schema: "Test",
                table: "Responses",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "SelectedChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsChoiceCorrect = table.Column<bool>(type: "bit", nullable: false),
                    ResponseUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedChoices_Responses_ResponseUserId",
                        column: x => x.ResponseUserId,
                        principalSchema: "Test",
                        principalTable: "Responses",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedChoices_ResponseUserId",
                table: "SelectedChoices",
                column: "ResponseUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedChoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responses",
                schema: "Test",
                table: "Responses");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Test",
                table: "Responses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChoiceId",
                schema: "Test",
                table: "Responses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsChoiceCorrect",
                schema: "Test",
                table: "Responses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                schema: "Test",
                table: "Responses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responses",
                schema: "Test",
                table: "Responses",
                column: "Id");
        }
    }
}
