using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;
using SharedLiberary.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repo
{
    public  class BookingRepository : Core.Repo.contract.IBookingRepository
    {
        private EventBookingDbContext _bookingDbContext;

        public BookingRepository(EventBookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }
        public async Task<BookingResponseDto> CreateBookingAsync(BookingDto bookingDto)
        {
            if (bookingDto == null)
                throw new ArgumentNullException(nameof(bookingDto));

            var userExists = await _bookingDbContext.Users.AnyAsync(u => u.Id == bookingDto.UserId);
            if (!userExists)
            {
                throw new Exception("The provided UserId does not exist.");
            }
            var booking = new Booking.Core.Models.Bookings
            {
                EventId = bookingDto.EventId,
                UserId = bookingDto.UserId,
                NumberOfSeats = bookingDto.NumberOfSeats,
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Pending"
            };

            await _bookingDbContext.Set<Booking.Core.Models.Bookings>().AddAsync(booking);
            await _bookingDbContext.SaveChangesAsync();

            // Load related data
            // Safely load related entities
            var eventLoaded = await _bookingDbContext.Events.FindAsync(booking.EventId);
            var userLoaded = await _bookingDbContext.Users.FindAsync(booking.UserId);

            if (eventLoaded == null || userLoaded == null)
            {
                throw new InvalidOperationException("Related event or user data could not be found after booking creation.");
            }

            return new BookingResponseDto
            {
                Id = booking.Id,
                BookingDate = booking.BookingDate,
                NumberOfSeats = booking.NumberOfSeats,
                BookingStatus = booking.BookingStatus,

                Event = new BEventDto
                {
                    Id = eventLoaded.Id,
                    Name = eventLoaded.Name,
                    Description = eventLoaded.Description,
                    ImageUrl = string.IsNullOrEmpty(eventLoaded.ImageName)
                    ? null
                    : $"/images/events/{eventLoaded.ImageName}",
                    Category = eventLoaded.Category,
                    Price = eventLoaded.Price,
                    Date = eventLoaded.Date,
                    Venue = eventLoaded.Venue
                },

                User = new BUserDto
                {
                    Id = userLoaded.Id,
                    Name = userLoaded.Name,
                    Email = userLoaded.Email,
                    Role = userLoaded.Role
                }
            };
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingDbContext.Set<Booking.Core.Models.Bookings>().FindAsync(bookingId);
            if (booking == null) return false;

            booking.BookingStatus = "Canceled";
            await _bookingDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Booking.Core.Models.Bookings> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingDbContext.FindAsync<Booking.Core.Models.Bookings>(bookingId);
        }

        public async Task<IEnumerable<Booking.Core.Models.Bookings>> GetBookingsByUserIdAsync(int userId)
        {
            return await _bookingDbContext.Set<Booking.Core.Models.Bookings>().Where(u => u.UserId == userId).ToListAsync();


        }

        public async Task<bool> UpdateBookingStatusAsync(int bookingId)
        {
            var booking = await _bookingDbContext.Set<Booking.Core.Models.Bookings>().FindAsync(bookingId);
            if (booking == null) return false;

            booking.BookingStatus = "Updated";
            await _bookingDbContext.SaveChangesAsync();
            return true;
        }
    }
}
