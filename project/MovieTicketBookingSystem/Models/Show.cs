using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models
{
    public class Show
    {
        public int ShowId { get; set; }

        public int MovieId { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }

        [Required]
        [Range(0, 1000)]
        public int AvailableSeats { get; set; }

        // Navigation properties
        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
