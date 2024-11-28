using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly LibraryDbContext _context;

        public ReservationController(LibraryDbContext context)
        {
            _context = context;
        }

        // Display Reservations for Logged-in User
        public IActionResult Index()
        {
            if (TempData["Username"] == null)
                return RedirectToAction("Login", "Account");

            string username = TempData["Username"].ToString();
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            var reservations = _context.Reservations
                .Where(r => r.UserId == user.Id)
                .Select(r => new
                {
                    r.Id,
                    r.Book.Title,
                    r.ReservationDate,
                    r.ExpiryDate
                }).ToList();

            return View(reservations);
        }

        // Reserve a Book
        public IActionResult Reserve(int bookId)
        {
            if (TempData["Username"] == null)
                return RedirectToAction("Login", "Account");

            var book = _context.Books.FirstOrDefault(b => b.Id == bookId && b.IsAvailable);
            if (book == null)
                return RedirectToAction("Index", "Book");

            string username = TempData["Username"].ToString();
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            var reservation = new Reservation
            {
                UserId = user.Id,
                BookId = book.Id,
                ReservationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(1)
            };

            book.IsAvailable = false;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        // Cancel a Reservation
        public IActionResult Cancel(int id)
        {
            if (TempData["Username"] == null)
                return RedirectToAction("Login", "Account");

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == reservation.BookId);
                if (book != null)
                    book.IsAvailable = true;

                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
