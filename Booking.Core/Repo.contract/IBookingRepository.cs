using SharedLiberary.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Repo.contract
{
    
    public interface IBookingRepository
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingDto bookingDto);
        Task<Booking.Core.Models.Bookings> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking.Core.Models.Bookings>> GetBookingsByUserIdAsync(int userId);
        Task<bool> UpdateBookingStatusAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
    }

}
