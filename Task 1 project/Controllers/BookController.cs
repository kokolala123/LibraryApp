using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.Where(b => !b.IsDeleted && b.IsAvailable).ToList();
            return View(books);
        }

        public IActionResult Create()
        {
            if (TempData["Role"]?.ToString() != "Librarian")
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.IsAvailable = true;
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            if (TempData["Role"]?.ToString() != "Librarian")
                return RedirectToAction("Index");

            var book = _context.Books.Find(id);
            if (book != null)
            {
                book.IsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
