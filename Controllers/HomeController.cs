using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
using CinemaApp.Models;

namespace CinemaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.IsFeatured)
                .OrderByDescending(m => m.ImdbRating)
                .Take(5)
                .ToListAsync();

            var recent = await _context.Movies
                .Include(m => m.Genre)
                .OrderByDescending(m => m.CreatedAt)
                .Take(8)
                .ToListAsync();

            var topRated = await _context.Movies
                .Include(m => m.Genre)
                .OrderByDescending(m => m.ImdbRating)
                .Take(6)
                .ToListAsync();

            var genres = await _context.Genres
                .Include(g => g.Movies)
                .ToListAsync();

            ViewBag.Featured = featured;
            ViewBag.Recent = recent;
            ViewBag.TopRated = topRated;
            ViewBag.Genres = genres;
            ViewBag.TotalMovies = await _context.Movies.CountAsync();
            ViewBag.TotalGenres = await _context.Genres.CountAsync();

            return View();
        }

        public IActionResult About() => View();

        public IActionResult Error()
        {
            return View();
        }
    }
}