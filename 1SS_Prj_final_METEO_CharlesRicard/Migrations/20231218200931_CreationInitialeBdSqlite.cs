using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prj_final_METEO.Migrations
{
    /// <inheritdoc />
    public partial class CreationInitialeBdSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[] { 1, 46.56984172477484, -72.738112856514419, "Shawinigan" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
