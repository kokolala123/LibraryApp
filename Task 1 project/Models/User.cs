using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Librarian" or "OrdinaryUser"

        // Navigation Properties
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

