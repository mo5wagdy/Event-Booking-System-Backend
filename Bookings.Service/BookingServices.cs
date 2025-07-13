using Booking.Core.Services.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Booking.Repo;
using SharedLiberary.DTOS;
using Booking.Core.Repo.contract;




namespace Bookings.Service
{

    public class BookingServices : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingServices(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingDto bookingDto)
        {
            if (bookingDto == null)
                throw new ArgumentNullException(nameof(bookingDto));

            return await _repository.CreateBookingAsync(bookingDto);
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            return await _repository.CancelBookingAsync(bookingId);
        }

        public async Task<Booking.Core.Models.Bookings> GetBookingByIdAsync(int bookingId)
        {
            return await _repository.GetBookingByIdAsync(bookingId);
        }

        public async Task<IEnumerable<Booking.Core.Models.Bookings>> GetBookingsByUserIdAsync(int userId)
        {
            return await _repository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<bool> UpdateBookingStatusAsync(int bookingId)
        {
            return await _repository.UpdateBookingStatusAsync(bookingId);
        }
    }
}
 



