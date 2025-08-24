using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StartMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    team_a = table.Column<string>(type: "TEXT", nullable: false),
                    team_b = table.Column<string>(type: "TEXT", nullable: false),
                    match_type = table.Column<string>(type: "TEXT", nullable: false),
                    price_ticket = table.Column<double>(type: "REAL", nullable: false),
                    number_of_seats_total = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    match_id = table.Column<long>(type: "INTEGER", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    number_of_seats_ticket = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticket_match_match_id",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "match",
                columns: new[] { "id", "match_type", "number_of_seats_total", "price_ticket", "team_a", "team_b" },
                values: new object[] { 1L, "GROUPS", 4000L, 300.0, "Barcelona", "Man Utd" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "password", "username" },
                values: new object[] { 1L, "$2a$11$hOvAZqJPHvV1vGVVl5RsPeouVWa1843xaPSgaZpJz1kjmWyIQDWva", "admin12345" });

            migrationBuilder.CreateIndex(
                name: "IX_ticket_match_id",
                table: "ticket",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "match");
        }
    }
}
