using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;

namespace CinemaApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action", Icon = "💥", Description = "Thrilling action movies" },
                new Genre { Id = 2, Name = "Drama", Icon = "🎭", Description = "Emotional drama films" },
                new Genre { Id = 3, Name = "Comedy", Icon = "😂", Description = "Funny comedy films" },
                new Genre { Id = 4, Name = "Horror", Icon = "👻", Description = "Thriller and horror films" },
                new Genre { Id = 5, Name = "Science Fiction", Icon = "🚀", Description = "Science fiction films" },
                new Genre { Id = 6, Name = "Romance", Icon = "❤️", Description = "Romantic films" },
                new Genre { Id = 7, Name = "Animation", Icon = "🎨", Description = "Animated films" },
                new Genre { Id = 8, Name = "Thriller", Icon = "🔪", Description = "Thriller films" }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1, Title = "Inception", Year = 2010, GenreId = 5,
                    Director = "Christopher Nolan",
                    Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                    ImdbRating = 8.8, Duration = 148, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",
                    IsFeatured = true, Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page"
                },
                new Movie
                {
                    Id = 2, Title = "The Dark Knight", Year = 2008, GenreId = 1,
                    Director = "Christopher Nolan",
                    Description = "Batman is forced to confront the Joker, who is trying to plunge Gotham City into chaos.",
                    ImdbRating = 9.0, Duration = 152, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg",
                    IsFeatured = true, Cast = "Christian Bale, Heath Ledger, Aaron Eckhart"
                },
                new Movie
                {
                    Id = 3, Title = "Interstellar", Year = 2014, GenreId = 5,
                    Director = "Christopher Nolan",
                    Description = "A team of astronauts travels through space searching for new habitable planets to secure the future of humanity.",
                    ImdbRating = 8.6, Duration = 169, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg",
                    IsFeatured = true, Cast = "Matthew McConaughey, Anne Hathaway, Jessica Chastain"
                },
                new Movie
                {
                    Id = 4, Title = "Parasite", Year = 2019, GenreId = 2,
                    Director = "Bong Joon-ho",
                    Description = "A poor family schemes to become employed by a wealthy household, leading to an unexpected confrontation.",
                    ImdbRating = 8.6, Duration = 132, Language = "Korean", Country = "South Korea",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg",
                    IsFeatured = false, Cast = "Song Kang-ho, Lee Sun-kyun, Cho Yeo-jeong"
                },
                new Movie
                {
                    Id = 5, Title = "The Shawshank Redemption", Year = 1994, GenreId = 2,
                    Director = "Frank Darabont",
                    Description = "A banker wrongly convicted of murder learns to survive in prison while maintaining hope for freedom.",
                    ImdbRating = 9.3, Duration = 142, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg",
                    IsFeatured = true, Cast = "Tim Robbins, Morgan Freeman, Bob Gunton"
                },
                new Movie
                {
                    Id = 6, Title = "Spirited Away", Year = 2001, GenreId = 7,
                    Director = "Hayao Miyazaki",
                    Description = "Ten-year-old Chihiro becomes trapped in a spirit world and must find a way to free herself and her parents.",
                    ImdbRating = 8.6, Duration = 125, Language = "Japanese", Country = "Japan",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg",
                    IsFeatured = false, Cast = "Daveigh Chase, Suzanne Pleshette, Miyu Irino"
                },
                new Movie
                {
                    Id = 7, Title = "Get Out", Year = 2017, GenreId = 4,
                    Director = "Jordan Peele",
                    Description = "A man uncovers terrifying secrets when he visits his girlfriend's mysterious family estate.",
                    ImdbRating = 7.7, Duration = 104, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/tFXcEccSQMf3lfhfXKSU9iRBpa3.jpg",
                    IsFeatured = false, Cast = "Daniel Kaluuya, Allison Williams, Bradley Whitford"
                },
                new Movie
                {
                    Id = 8, Title = "The Grand Budapest Hotel", Year = 2014, GenreId = 3,
                    Director = "Wes Anderson",
                    Description = "The comedic adventures of a legendary hotel concierge and his young apprentice are recounted.",
                    ImdbRating = 8.1, Duration = 99, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/eWdyYQreja6JGCzqHWXpWHDrrPo.jpg",
                    IsFeatured = false, Cast = "Ralph Fiennes, F. Murray Abraham, Mathieu Amalric"
                },
                new Movie
                {
                    Id = 9, Title = "Dune", Year = 2021, GenreId = 5,
                    Director = "Denis Villeneuve",
                    Description = "An epic science fiction film about the war over control of a dangerous desert planet containing a precious resource.",
                    ImdbRating = 8.0, Duration = 155, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/d5NXSklXo0qyIYkgV94XAgMIckC.jpg",
                    IsFeatured = true, Cast = "Timothée Chalamet, Rebecca Ferguson, Oscar Isaac"
                },
                new Movie
                {
                    Id = 10, Title = "La La Land", Year = 2016, GenreId = 6,
                    Director = "Damien Chazelle",
                    Description = "A love story between two young dreamers chasing their ambitions in Los Angeles.",
                    ImdbRating = 8.0, Duration = 128, Language = "English", Country = "USA",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/uDO8zWDhfWwoFdKS4fzkUJt0Rf0.jpg",
                    IsFeatured = false, Cast = "Ryan Gosling, Emma Stone, John Legend"
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, MovieId = 1, Content = "Mind-blowing, an incredible film!", Rating = 9, ReviewerName = "Ahmet K.", CreatedAt = new DateTime(2024, 1, 10) },
                new Review { Id = 2, MovieId = 1, Content = "One of Nolan's masterpieces.", Rating = 10, ReviewerName = "Elif Y.", CreatedAt = new DateTime(2024, 2, 5) },
                new Review { Id = 3, MovieId = 2, Content = "Heath Ledger's Joker performance was unbelievable.", Rating = 10, ReviewerName = "Mehmet A.", CreatedAt = new DateTime(2024, 1, 15) },
                new Review { Id = 4, MovieId = 3, Content = "The visual effects are incredibly impressive.", Rating = 8, ReviewerName = "Zeynep T.", CreatedAt = new DateTime(2024, 3, 20) },
                new Review { Id = 5, MovieId = 5, Content = "The greatest film of all time.", Rating = 10, ReviewerName = "Ali R.", CreatedAt = new DateTime(2024, 2, 28) }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@cinemaapp.com",
                    PasswordHash = BCryptHash("Admin123!"),
                    IsAdmin = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );
        }

        private static string BCryptHash(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + "salt_cinema_2024");
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}