using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;

namespace CinemaApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) => _context = context;

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "true";

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.TotalMovies = await _context.Movies.CountAsync();
            ViewBag.TotalGenres = await _context.Genres.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.TotalReviews = await _context.Reviews.CountAsync();

            var movies = await _context.Movies.Include(m => m.Genre).OrderByDescending(m => m.CreatedAt).ToListAsync();
            var genres = await _context.Genres.Include(g => g.Movies).ToListAsync();
            var users = await _context.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
            var reviews = await _context.Reviews.Include(r => r.Movie).OrderByDescending(r => r.CreatedAt).ToListAsync();

            ViewBag.Movies = movies;
            ViewBag.Genres = genres;
            ViewBag.Users = users;
            ViewBag.Reviews = reviews;

            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!IsAdmin()) return Forbid();
            var review = await _context.Reviews.FindAsync(id);
            if (review != null) { _context.Reviews.Remove(review); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Review deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}