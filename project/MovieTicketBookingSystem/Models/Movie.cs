using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        [StringLength(100)]
        public string MovieName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = null!;

        [Required]
        [Range(1, 500)]
        public int Duration { get; set; } // Duration in minutes

        // Navigation property
        public ICollection<Show> Shows { get; set; } = new List<Show>();
    }
}
