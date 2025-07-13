using Booking.Core.Services.contract;
using Moq;
using SharedLiberary.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookings.Service;
using System.Threading.Tasks;
using Bookings.Service;

namespace unitTesting.ServiceLayer
{
  
        public class BookingServiceTests
        {
            private readonly Mock<Booking.Core.Repo.contract.IBookingRepository> _mockRepo;
            private readonly IBookingService _bookingService;

            public BookingServiceTests()
            {
                _mockRepo = new Mock<Booking.Core.Repo.contract.IBookingRepository>();
                _bookingService = new BookingServices(_mockRepo.Object);
            }
        [Fact]
        public async Task CreateBookingAsync_ReturnsBookingResponse_WhenValidInput()
        {
            // Arrange
            var bookingDto = new BookingDto { EventId = 1, UserId = 10, NumberOfSeats = 2 };
            var expectedResponse = new BookingResponseDto { BookingStatus = "Pending" };

            _mockRepo.Setup(r => r.CreateBookingAsync(bookingDto))
                     .ReturnsAsync(expectedResponse);

            // Act
            var result = await _bookingService.CreateBookingAsync(bookingDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pending", result.BookingStatus);
        }

        [Fact]
        public async Task CancelBookingAsync_ReturnsTrue_WhenBookingExists()
        {
            // Arrange
            int bookingId = 1;
            _mockRepo.Setup(r => r.CancelBookingAsync(bookingId)).ReturnsAsync(true);

            // Act
            var result = await _bookingService.CancelBookingAsync(bookingId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetBookingByIdAsync_ReturnsBooking_WhenFound()
        {
            // Arrange
            int bookingId = 1;
            var booking = new Booking.Core.Models.Bookings { Id = bookingId, UserId = 10 };
            _mockRepo.Setup(r => r.GetBookingByIdAsync(bookingId)).ReturnsAsync(booking);

            // Act
            var result = await _bookingService.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookingId, result.Id);
        }

        [Fact]
        public async Task UpdateBookingStatusAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            int bookingId = 1;
            _mockRepo.Setup(r => r.UpdateBookingStatusAsync(bookingId)).ReturnsAsync(true);

            // Act
            var result = await _bookingService.UpdateBookingStatusAsync(bookingId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetBookingsByUserIdAsync_ReturnsBookingsList_WhenExists()
        {
            // Arrange
            int userId = 10;
            var bookingsList = new List<Booking.Core.Models.Bookings>
    {
        new Booking.Core.Models.Bookings { Id = 1, UserId = userId },
        new Booking.Core.Models.Bookings { Id = 2, UserId = userId }
    };

            _mockRepo.Setup(r => r.GetBookingsByUserIdAsync(userId)).ReturnsAsync(bookingsList);

            // Act
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(1, item.Id),
                item => Assert.Equal(2, item.Id));
        }


    }
}



