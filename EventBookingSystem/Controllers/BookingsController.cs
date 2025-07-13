using Booking.Core.Models;
using Booking.Core.Services.contract;
using  SharedLiberary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase

    {

        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateBooking")]
        [Authorize]

        public async Task<ActionResult<Booking.Core.Models.Bookings>> CreateBookingAaync(BookingDto bookingDto)
        {
           
                if (bookingDto == null || bookingDto.UserId <= 0)
                {
                    return BadRequest(new { Errors = new List<string> { "Invalid booking data or missing UserId." } });
                }

                var result = await _bookingService.CreateBookingAsync(bookingDto);
                return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, result);
            }

        [HttpGet("GetBooking/{id}")]
        public async Task<ActionResult<Booking.Core.Models.Bookings>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPut("UpdateBookingStatus/{id}")]
        public async Task<ActionResult> UpdateBookingStatus(int id)
        {
            var success = await _bookingService.UpdateBookingStatusAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("CancelBooking/{id}")]
        public async Task<ActionResult> CancelBooking(int id)
        {
            var success = await _bookingService.CancelBookingAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }







    } }

      


       
