using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
    }
}
