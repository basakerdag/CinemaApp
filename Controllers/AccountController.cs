using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
using CinemaApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace CinemaApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context) => _context = context;

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + "salt_cinema_2024");
            return Convert.ToBase64String(sha.ComputeHash(bytes));
        }

        public IActionResult Login() => View();

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var hash = HashPassword(password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");

            TempData["Success"] = $"Welcome, {user.Username}!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                ViewBag.Error = "This username is already taken.";
                return View();
            }

            _context.Users.Add(new AppUser
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                IsAdmin = false
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Registration successful! You can now log in.";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}