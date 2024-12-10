using System;

namespace LibraryApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime DateOfPublication { get; set; }
        public decimal Price { get; set; }

        // Add these properties
        public bool IsDeleted { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
    }

