using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RSAllies.Users.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderType = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseClasses",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsForeigner = table.Column<bool>(type: "bit", nullable: false),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalSchema: "Users",
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "Users",
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_LicenseClasses_LicenseClassId",
                        column: x => x.LicenseClassId,
                        principalSchema: "Users",
                        principalTable: "LicenseClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalSchema: "Users",
                        principalTable: "Nationalities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Users",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportCases",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CaseNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportCases_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "EducationLevels",
                columns: new[] { "Id", "Level" },
                values: new object[,]
                {
                    { new Guid("0c0f424e-89c0-4b59-ab2c-b45fa44a9c5c"), "Uneducated" },
                    { new Guid("414c1743-2fd5-416b-a442-3bbc9a620aef"), "PHD" },
                    { new Guid("5449d74f-30a8-4439-9568-79d9e58861c5"), "Diploma" },
                    { new Guid("5f51730c-518b-4aa2-8cd8-c61da2517585"), "Form 2" },
                    { new Guid("7751497b-7924-42cf-8dca-3d44b89d7ce5"), "Bachelor's Degree" },
                    { new Guid("9efeba97-bb9a-48b3-af07-803e79a4f800"), "Form 4" },
                    { new Guid("a0770ebc-12e9-48f9-90d9-9b5cfc594e31"), "Class 7" },
                    { new Guid("c8b60d80-54c3-4a19-bef6-58ad0ea2e9fa"), "Master's Degree" },
                    { new Guid("ef3cd9db-b8cf-4faf-bfbb-ec10f0fe99b0"), "Form 6" }
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Genders",
                columns: new[] { "Id", "GenderType" },
                values: new object[,]
                {
                    { new Guid("ee2ca06a-6f3c-4d91-8bff-f3f8eb4a7935"), "Male" },
                    { new Guid("fd1873e0-cf3e-4c82-875c-273e96d24828"), "Female" }
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "LicenseClasses",
                columns: new[] { "Id", "Class" },
                values: new object[,]
                {
                    { new Guid("0a9a6209-2f0a-4820-8bdb-d51509de26f1"), "Class A" },
                    { new Guid("13142fca-8d24-48db-9947-f63c092e5314"), "Class E" },
                    { new Guid("13aef938-5b37-4bab-8dd4-c37583d9a8ff"), "Class A3" },
                    { new Guid("3c530344-75cd-4a0c-bf7f-3dc09fc2b9a9"), "Class G" },
                    { new Guid("47324fb5-3e99-4e85-aa21-6b29d5156c02"), "Class F" },
                    { new Guid("516cc8d3-979b-41e3-8e87-6670c4723a17"), "Class D" },
                    { new Guid("59c5fb75-8975-4202-a47f-0cde3096e8d3"), "Class A2" },
                    { new Guid("76aaf2b9-69ce-447a-a2e5-8c52ce8f15cd"), "Class C3" },
                    { new Guid("c2894fe3-419d-4548-ad53-f2b11f209167"), "Class A1" },
                    { new Guid("c81f325d-3d31-41ac-8545-53b2f63c1aa5"), "Class C2" },
                    { new Guid("ca0e9c3e-ef35-46fc-8c44-49e1a2ac7326"), "Class C1" },
                    { new Guid("dea89baa-d4cc-4969-bf52-01e3113c2be7"), "Class B" },
                    { new Guid("ee9aa063-7b60-492c-9089-020845e92b95"), "Class C" }
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("30c317cf-05ce-480a-9349-0dfcfc30e150"), "Manager" },
                    { new Guid("89cf538f-db69-4442-bff8-99acc0bd1e8c"), "SuperUser" },
                    { new Guid("8dd927de-1ccf-4582-9519-73b577855175"), "Administrator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_FirstName_LastName",
                schema: "Users",
                table: "Administrators",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_RoleId",
                schema: "Users",
                table: "Administrators",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_Username",
                schema: "Users",
                table: "Administrators",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportCases_UserId",
                schema: "Users",
                table: "SupportCases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationLevelId",
                schema: "Users",
                table: "Users",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirstName_MiddleName_LastName",
                schema: "Users",
                table: "Users",
                columns: new[] { "FirstName", "MiddleName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                schema: "Users",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Identification",
                schema: "Users",
                table: "Users",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LicenseClassId",
                schema: "Users",
                table: "Users",
                column: "LicenseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalityId",
                schema: "Users",
                table: "Users",
                column: "NationalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Administrators",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "SupportCases",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "EducationLevels",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "LicenseClasses",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Nationalities",
                schema: "Users");
        }
    }
}
