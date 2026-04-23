using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly MovieContext _context;

        public BookingController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Movie)
                .Include(b => b.Show)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Create(int showId)
        {
            var show = await _context.Shows
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.ShowId == showId);

            if (show == null) return NotFound();

            var booking = new Booking
            {
                ShowId = show.ShowId,
                MovieId = show.MovieId,
                SeatsBooked = 1
            };

            ViewBag.ShowDetails = show;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,ShowId,UserName,MobileNumber,SeatsBooked")] Booking booking)
        {
            var show = await _context.Shows
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.ShowId == booking.ShowId);

            if (show == null) return NotFound();

            if (booking.SeatsBooked > show.AvailableSeats)
            {
                ModelState.AddModelError("SeatsBooked", $"Only {show.AvailableSeats} seats are available.");
            }

            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.Now;
                show.AvailableSeats -= booking.SeatsBooked;

                _context.Add(booking);
                _context.Update(show);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }

            ViewBag.ShowDetails = show;
            return View(booking);
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Movie)
                .Include(b => b.Show)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
    }
}
