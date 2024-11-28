using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; } // If true, it is marked as unavailable

        // Navigation Properties
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
