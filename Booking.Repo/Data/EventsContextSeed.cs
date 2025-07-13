using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
///
namespace Booking.Repo.Data
{
   
            public class EventsContextSeed
           {
            public async static Task SeedAsync(EventBookingDbContext _dbcontext)
            {
                if (!_dbcontext.Users.Any())
                {
                    var usersData = File.ReadAllText("../Booking.Repo/Data/DataSeeding/Users.json");
                    var users = JsonSerializer.Deserialize<List<Users>>(usersData);

                    if (users?.Count > 0)
                    {
                        _dbcontext.Users.AddRange(users);
                    try
                    {
                        await _dbcontext.SaveChangesAsync();
                    }
                   
                     catch (DbUpdateException ex)
                    {
                    Console.WriteLine("❌ DbUpdateException: " + ex.Message);

                    if (ex.InnerException != null)
                        Console.WriteLine("👉 Inner Exception: " + ex.InnerException.Message);

                    throw; // Optional: rethrow if you want to see full trace
                }

            }
                }

                if (!_dbcontext.Events.Any())
                {
                    var eventsData = File.ReadAllText("../Booking.Repo/Data/DataSeeding/Events.json");
                    var events = JsonSerializer.Deserialize<List<Events>>(eventsData);

                    if (events?.Count > 0)
                    {
                        _dbcontext.Events.AddRange(events);
                        await _dbcontext.SaveChangesAsync();
                    }
                }

                if (!_dbcontext.Bookings.Any())
                {
                    var bookingsData = File.ReadAllText("../Booking.Repo/Data/DataSeeding/Bookings.json");
                    var bookings = JsonSerializer.Deserialize<List<Bookings>>(bookingsData);

                    if (bookings?.Count > 0)
                    {
                        _dbcontext.Bookings.AddRange(bookings);
                        await _dbcontext.SaveChangesAsync();
                    }
                }
            }
        }



    }


