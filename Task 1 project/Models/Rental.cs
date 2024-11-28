using System;

namespace LibraryApp.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
