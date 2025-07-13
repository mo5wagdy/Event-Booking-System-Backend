using Microsoft.EntityFrameworkCore;
using Booking.Core.Models;

namespace Booking.Repo
{
    public class EventBookingDbContext : DbContext
    {
        public EventBookingDbContext(DbContextOptions<EventBookingDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
      









    }
}
