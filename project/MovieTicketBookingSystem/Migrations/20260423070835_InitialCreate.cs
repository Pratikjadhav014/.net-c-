using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowId);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatsBooked = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Duration", "Genre", "MovieName" },
                values: new object[,]
                {
                    { 1, 148, "Sci-Fi", "Inception" },
                    { 2, 152, "Action", "The Dark Knight" },
                    { 3, 169, "Sci-Fi", "Interstellar" },
                    { 4, 195, "Romance", "Titanic" },
                    { 5, 162, "Sci-Fi", "Avatar" }
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "ShowId", "AvailableSeats", "MovieId", "ShowTime" },
                values: new object[,]
                {
                    { 1, 100, 1, new DateTime(2026, 4, 24, 10, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 100, 1, new DateTime(2026, 4, 24, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 150, 2, new DateTime(2026, 4, 24, 11, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 150, 2, new DateTime(2026, 4, 24, 18, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 120, 3, new DateTime(2026, 4, 25, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 6, 120, 3, new DateTime(2026, 4, 25, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 7, 200, 4, new DateTime(2026, 4, 24, 16, 0, 0, 0, DateTimeKind.Local) },
                    { 8, 200, 4, new DateTime(2026, 4, 25, 15, 0, 0, 0, DateTimeKind.Local) },
                    { 9, 180, 5, new DateTime(2026, 4, 26, 13, 0, 0, 0, DateTimeKind.Local) },
                    { 10, 180, 5, new DateTime(2026, 4, 26, 19, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MovieId",
                table: "Bookings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShowId",
                table: "Bookings",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieId",
                table: "Shows",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
