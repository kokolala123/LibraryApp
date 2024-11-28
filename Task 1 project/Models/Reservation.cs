using System;

namespace LibraryApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime ReservationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
