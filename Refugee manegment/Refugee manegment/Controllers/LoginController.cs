using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refugee_manegment.Models;

namespace Refugee_manegment.Controllers
{
    public class LoginController : Controller
    {
        private readonly WebDbContext _context;

        public LoginController(WebDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                // Find user in the database
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Directly redirect to the home page without storing session or cookies
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Please fill out all required fields.";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Simply redirect to the login page without clearing session (since no session is used)
            return RedirectToAction("Login", "Login");
        }
    }
}
