using System.Collections.Generic;

namespace LibraryApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Default role is "User"
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
