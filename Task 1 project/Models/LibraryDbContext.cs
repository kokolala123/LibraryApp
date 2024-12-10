using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        // Add this constructor
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });
        }
    }
}
