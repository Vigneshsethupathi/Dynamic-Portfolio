using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class skills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Programming_skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Web_Development = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cloud_Computing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Analysis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Communication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teamwork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Leadership = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Additional_Skill = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "skills");
        }
    }
}
