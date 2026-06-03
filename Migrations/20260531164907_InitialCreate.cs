using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Director = table.Column<string>(type: "TEXT", nullable: true),
                    Cast = table.Column<string>(type: "TEXT", nullable: true),
                    ImdbRating = table.Column<double>(type: "REAL", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    PosterUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TrailerUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReviewerName = table.Column<string>(type: "TEXT", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Thrilling action movies", "💥", "Action" },
                    { 2, "Emotional drama films", "🎭", "Drama" },
                    { 3, "Funny comedy films", "😂", "Comedy" },
                    { 4, "Thriller and horror films", "👻", "Horror" },
                    { 5, "Science fiction films", "🚀", "Science Fiction" },
                    { 6, "Romantic films", "❤️", "Romance" },
                    { 7, "Animated films", "🎨", "Animation" },
                    { 8, "Thriller films", "🔪", "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "PasswordHash", "Username" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@cinemaapp.com", true, "joTvPLlCgdhD4w6AQ+b/ysBq7BqBZWsOTyAkH5sM7Rg=", "admin" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Cast", "Country", "CreatedAt", "Description", "Director", "Duration", "GenreId", "ImdbRating", "IsFeatured", "Language", "PosterUrl", "Title", "TrailerUrl", "Year" },
                values: new object[,]
                {
                    { 1, "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1864), "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", "Christopher Nolan", 148, 5, 8.8000000000000007, true, "English", "https://image.tmdb.org/t/p/w500/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", "Inception", null, 2010 },
                    { 2, "Christian Bale, Heath Ledger, Aaron Eckhart", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1885), "Batman is forced to confront the Joker, who is trying to plunge Gotham City into chaos.", "Christopher Nolan", 152, 1, 9.0, true, "English", "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg", "The Dark Knight", null, 2008 },
                    { 3, "Matthew McConaughey, Anne Hathaway, Jessica Chastain", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1889), "A team of astronauts travels through space searching for new habitable planets to secure the future of humanity.", "Christopher Nolan", 169, 5, 8.5999999999999996, true, "English", "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", "Interstellar", null, 2014 },
                    { 4, "Song Kang-ho, Lee Sun-kyun, Cho Yeo-jeong", "South Korea", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1891), "A poor family schemes to become employed by a wealthy household, leading to an unexpected confrontation.", "Bong Joon-ho", 132, 2, 8.5999999999999996, false, "Korean", "https://image.tmdb.org/t/p/w500/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg", "Parasite", null, 2019 },
                    { 5, "Tim Robbins, Morgan Freeman, Bob Gunton", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1894), "A banker wrongly convicted of murder learns to survive in prison while maintaining hope for freedom.", "Frank Darabont", 142, 2, 9.3000000000000007, true, "English", "https://image.tmdb.org/t/p/w500/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg", "The Shawshank Redemption", null, 1994 },
                    { 6, "Daveigh Chase, Suzanne Pleshette, Miyu Irino", "Japan", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1896), "Ten-year-old Chihiro becomes trapped in a spirit world and must find a way to free herself and her parents.", "Hayao Miyazaki", 125, 7, 8.5999999999999996, false, "Japanese", "https://image.tmdb.org/t/p/w500/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg", "Spirited Away", null, 2001 },
                    { 7, "Daniel Kaluuya, Allison Williams, Bradley Whitford", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1899), "A man uncovers terrifying secrets when he visits his girlfriend's mysterious family estate.", "Jordan Peele", 104, 4, 7.7000000000000002, false, "English", "https://image.tmdb.org/t/p/w500/tFXcEccSQMf3lfhfXKSU9iRBpa3.jpg", "Get Out", null, 2017 },
                    { 8, "Ralph Fiennes, F. Murray Abraham, Mathieu Amalric", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1902), "The comedic adventures of a legendary hotel concierge and his young apprentice are recounted.", "Wes Anderson", 99, 3, 8.0999999999999996, false, "English", "https://image.tmdb.org/t/p/w500/eWdyYQreja6JGCzqHWXpWHDrrPo.jpg", "The Grand Budapest Hotel", null, 2014 },
                    { 9, "Timothée Chalamet, Rebecca Ferguson, Oscar Isaac", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1904), "An epic science fiction film about the war over control of a dangerous desert planet containing a precious resource.", "Denis Villeneuve", 155, 5, 8.0, true, "English", "https://image.tmdb.org/t/p/w500/d5NXSklXo0qyIYkgV94XAgMIckC.jpg", "Dune", null, 2021 },
                    { 10, "Ryan Gosling, Emma Stone, John Legend", "USA", new DateTime(2026, 5, 31, 19, 49, 7, 55, DateTimeKind.Local).AddTicks(1907), "A love story between two young dreamers chasing their ambitions in Los Angeles.", "Damien Chazelle", 128, 6, 8.0, false, "English", "https://image.tmdb.org/t/p/w500/uDO8zWDhfWwoFdKS4fzkUJt0Rf0.jpg", "La La Land", null, 2016 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CreatedAt", "MovieId", "Rating", "ReviewerName" },
                values: new object[,]
                {
                    { 1, "Mind-blowing, an incredible film!", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, "Ahmet K." },
                    { 2, "One of Nolan's masterpieces.", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10, "Elif Y." },
                    { 3, "Heath Ledger's Joker performance was unbelievable.", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, "Mehmet A." },
                    { 4, "The visual effects are incredibly impressive.", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, "Zeynep T." },
                    { 5, "The greatest film of all time.", new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 10, "Ali R." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_MovieId",
                table: "Favorites",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
