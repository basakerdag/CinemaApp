using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie title is required.")]
        [StringLength(200)]
        [Display(Name = "Movie Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Year")]
        [Range(1888, 2100)]
        public int Year { get; set; }

        [Display(Name = "Director")]
        public string? Director { get; set; }

        [Display(Name = "Cast")]
        public string? Cast { get; set; }

        [Display(Name = "IMDB Rating")]
        [Range(0.0, 10.0)]
        public double? ImdbRating { get; set; }

        [Display(Name = "Duration (min)")]
        public int? Duration { get; set; }

        [Display(Name = "Language")]
        public string? Language { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Poster URL")]
        public string? PosterUrl { get; set; }

        [Display(Name = "Trailer URL")]
        public string? TrailerUrl { get; set; }

        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Date Added")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int GenreId { get; set; }

        [Display(Name = "Genre")]
        public Genre? Genre { get; set; }

        // Navigation
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}