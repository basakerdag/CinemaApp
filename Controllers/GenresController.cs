using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
using CinemaApp.Models;

namespace CinemaApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GenresController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres
                .Include(g => g.Movies)
                .ToListAsync();
            return View(genres);
        }

        public async Task<IActionResult> Details(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "true";

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Genre added!";
                return RedirectToAction("Index", "Admin");
            }
            return View(genre);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null) { _context.Genres.Remove(genre); await _context.SaveChangesAsync(); }
            return RedirectToAction("Index", "Admin");
        }
    }
}