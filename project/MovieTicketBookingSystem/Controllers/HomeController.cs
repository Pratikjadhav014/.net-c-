using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieContext _context;

    public HomeController(ILogger<HomeController> logger, MovieContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalMovies = await _context.Movies.CountAsync();
        var totalBookings = await _context.Bookings.CountAsync();
        var totalShows = await _context.Shows.CountAsync();

        ViewBag.TotalMovies = totalMovies;
        ViewBag.TotalBookings = totalBookings;
        ViewBag.TotalShows = totalShows;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
