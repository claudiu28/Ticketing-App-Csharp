using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StartMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "match",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "match",
                columns: new[] { "id", "match_type", "number_of_seats_total", "price_ticket", "team_a", "team_b" },
                values: new object[] { 1L, "GROUPS", 4000L, 300.0, "Barcelona", "Man Utd" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "password", "username" },
                values: new object[] { 1L, "$2a$11$hOvAZqJPHvV1vGVVl5RsPeouVWa1843xaPSgaZpJz1kjmWyIQDWva", "admin12345" });
        }
    }
}
