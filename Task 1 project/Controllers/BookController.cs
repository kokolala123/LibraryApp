using LibraryApp.Models; // Include your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        // Constructor for dependency injection
        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public IActionResult Index()
        {
            var books = _context.Books
                .Where(b => !b.IsDeleted) // Only show books that are not deleted
                .ToList();
            return View(books);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedBook);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Books.Any(b => b.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updatedBook);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                // Mark as deleted instead of removing from the database
                book.IsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
