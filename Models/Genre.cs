using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Genre Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Icon (emoji)")]
        public string? Icon { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }

    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Review")]
        public string Content { get; set; } = string.Empty;

        [Range(1, 10)]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Display(Name = "Review Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Username")]
        public string ReviewerName { get; set; } = string.Empty;

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }

    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }

    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser? User { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}