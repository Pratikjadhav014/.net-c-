using System;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Show> Shows { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { MovieId = 1, MovieName = "Inception", Genre = "Sci-Fi", Duration = 148 },
                new Movie { MovieId = 2, MovieName = "The Dark Knight", Genre = "Action", Duration = 152 },
                new Movie { MovieId = 3, MovieName = "Interstellar", Genre = "Sci-Fi", Duration = 169 },
                new Movie { MovieId = 4, MovieName = "Titanic", Genre = "Romance", Duration = 195 },
                new Movie { MovieId = 5, MovieName = "Avatar", Genre = "Sci-Fi", Duration = 162 }
            );

            // Seed Shows
            modelBuilder.Entity<Show>().HasData(
                new Show { ShowId = 1, MovieId = 1, ShowTime = DateTime.Today.AddDays(1).AddHours(10), AvailableSeats = 100 },
                new Show { ShowId = 2, MovieId = 1, ShowTime = DateTime.Today.AddDays(1).AddHours(14), AvailableSeats = 100 },
                new Show { ShowId = 3, MovieId = 2, ShowTime = DateTime.Today.AddDays(1).AddHours(11), AvailableSeats = 150 },
                new Show { ShowId = 4, MovieId = 2, ShowTime = DateTime.Today.AddDays(1).AddHours(18), AvailableSeats = 150 },
                new Show { ShowId = 5, MovieId = 3, ShowTime = DateTime.Today.AddDays(2).AddHours(12), AvailableSeats = 120 },
                new Show { ShowId = 6, MovieId = 3, ShowTime = DateTime.Today.AddDays(2).AddHours(20), AvailableSeats = 120 },
                new Show { ShowId = 7, MovieId = 4, ShowTime = DateTime.Today.AddDays(1).AddHours(16), AvailableSeats = 200 },
                new Show { ShowId = 8, MovieId = 4, ShowTime = DateTime.Today.AddDays(2).AddHours(15), AvailableSeats = 200 },
                new Show { ShowId = 9, MovieId = 5, ShowTime = DateTime.Today.AddDays(3).AddHours(13), AvailableSeats = 180 },
                new Show { ShowId = 10, MovieId = 5, ShowTime = DateTime.Today.AddDays(3).AddHours(19), AvailableSeats = 180 }
            );
        }
    }
}
