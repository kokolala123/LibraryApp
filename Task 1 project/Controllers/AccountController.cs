using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly LibraryDbContext _context;

        public AccountController(LibraryDbContext context)
        {
            _context = context;
        }

        // Registration View
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(user);
                }

                // Save the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }


        // Handle Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Set session or claim (simple example with TempData)
                TempData["Username"] = user.Username;
                TempData["Role"] = user.Role;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid username or password.";
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }
    }
}
