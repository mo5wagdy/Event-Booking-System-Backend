using Booking.Core.Models;
using SharedLiberary.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace Booking.Core.Services.contract
{
    public  interface IBookingService
    {
       Task<BookingResponseDto> CreateBookingAsync(BookingDto bookingDto);
        Task<Booking.Core.Models.Bookings> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking.Core.Models.Bookings>> GetBookingsByUserIdAsync(int userId);
      
        Task<bool> UpdateBookingStatusAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
