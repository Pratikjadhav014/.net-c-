using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Controllers
{
    public class ShowController : Controller
    {
        private readonly MovieContext _context;

        public ShowController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movieContext = _context.Shows.Include(s => s.Movie);
            return View(await movieContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShowId,MovieId,ShowTime,AvailableSeats")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieName", show.MovieId);
            return View(show);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var show = await _context.Shows.FindAsync(id);
            if (show == null) return NotFound();

            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieName", show.MovieId);
            return View(show);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShowId,MovieId,ShowTime,AvailableSeats")] Show show)
        {
            if (id != show.ShowId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.ShowId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieName", show.MovieId);
            return View(show);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var show = await _context.Shows
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.ShowId == id);
            
            if (show == null) return NotFound();

            return View(show);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                _context.Shows.Remove(show);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.ShowId == id);
        }
    }
}
