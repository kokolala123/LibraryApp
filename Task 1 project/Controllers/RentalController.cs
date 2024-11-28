using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryApp.Controllers
{
    public class RentalController : Controller
    {
        private readonly LibraryDbContext _context;

        public RentalController(LibraryDbContext context)
        {
            _context = context;
        }

        // View All Rentals
        public IActionResult Index()
        {
            if (TempData["Role"]?.ToString() != "Librarian")
                return RedirectToAction("Login", "Account");

            var rentals = _context.Rentals.Select(r => new
            {
                r.Id,
                r.Book.Title,
                r.User.Username,
                r.RentalDate,
                r.ReturnDate
            }).ToList();

            return View(rentals);
        }

        // Convert Reservation to Rental
        public IActionResult ConvertToRental(int reservationId)
        {
            if (TempData["Role"]?.ToString() != "Librarian")
                return RedirectToAction("Login", "Account");

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
                return RedirectToAction("Index", "Reservation");

            var rental = new Rental
            {
                UserId = reservation.UserId,
                BookId = reservation.BookId,
                RentalDate = DateTime.Now
            };

            var book = _context.Books.FirstOrDefault(b => b.Id == reservation.BookId);
            if (book != null)
                book.IsAvailable = false;

            _context.Rentals.Add(rental);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index", "Reservation");
        }

        // Mark a Book as Returned
        public IActionResult MarkAsReturned(int id)
        {
            if (TempData["Role"]?.ToString() != "Librarian")
                return RedirectToAction("Login", "Account");

            var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
            if (rental != null)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == rental.BookId);
                if (book != null)
                    book.IsAvailable = true;

                rental.ReturnDate = DateTime.Now;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
