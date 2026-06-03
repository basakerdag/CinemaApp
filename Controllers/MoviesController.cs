using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
using CinemaApp.Models;

namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, int? genreId, string? sort, int page = 1)
        {
            int pageSize = 9;
            var query = _context.Movies.Include(m => m.Genre).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.Title.Contains(search) || m.Director!.Contains(search));

            if (genreId.HasValue)
                query = query.Where(m => m.GenreId == genreId);

            query = sort switch
            {
                "rating" => query.OrderByDescending(m => m.ImdbRating),
                "year_asc" => query.OrderBy(m => m.Year),
                "year_desc" => query.OrderByDescending(m => m.Year),
                "title" => query.OrderBy(m => m.Title),
                _ => query.OrderByDescending(m => m.CreatedAt)
            };

            int totalCount = await query.CountAsync();
            var movies = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name");
            ViewBag.Search = search;
            ViewBag.GenreId = genreId;
            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.TotalCount = totalCount;

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            var similar = await _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.GenreId == movie.GenreId && m.Id != id)
                .OrderByDescending(m => m.ImdbRating)
                .Take(4)
                .ToListAsync();

            ViewBag.Similar = similar;
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int movieId, string content, int rating, string reviewerName)
        {
            if (!string.IsNullOrEmpty(content) && rating >= 1 && rating <= 10)
            {
                _context.Reviews.Add(new Review
                {
                    MovieId = movieId,
                    Content = content,
                    Rating = rating,
                    ReviewerName = string.IsNullOrEmpty(reviewerName) ? "Anonymous" : reviewerName
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = movieId });
        }

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "true";

        public async Task<IActionResult> Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            ViewBag.Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                movie.CreatedAt = DateTime.Now;
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Movie added successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name");
            return View(movie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            ViewBag.Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id != movie.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Movie updated!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Movie deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}