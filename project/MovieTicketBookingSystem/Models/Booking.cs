using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int MovieId { get; set; }

        public int ShowId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string MobileNumber { get; set; } = null!;

        [Required]
        [Range(1, 100, ErrorMessage = "Must book at least 1 seat")]
        public int SeatsBooked { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }

        [ForeignKey("ShowId")]
        public Show? Show { get; set; }
    }
}
