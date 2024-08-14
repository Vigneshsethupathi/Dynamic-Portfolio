using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class projectproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_LOGO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Starting_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ending_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Using_Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Using_Framwork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Using_Database = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    User_emailIDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projects_users_User_emailIDId",
                        column: x => x.User_emailIDId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projects_User_emailIDId",
                table: "projects",
                column: "User_emailIDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
