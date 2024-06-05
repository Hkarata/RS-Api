﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSAllies.Test.Migrations
{
    /// <inheritdoc />
    public partial class InitialModifMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Test",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Test",
                table: "Questions");
        }
    }
}
