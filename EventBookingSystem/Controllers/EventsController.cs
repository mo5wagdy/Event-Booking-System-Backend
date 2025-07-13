using AutoMapper;
using Booking.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.Core.Services.contract;   
using SharedLiberary.DTOS;
using Booking.Core.Repo.contract;
namespace EventBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IGenericRepository<Events> _repository;
        private readonly IMapper _mapper;

        public EventsController(IGenericRepository<Events> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetAllEventsAsync()
        {
            var events = await _repository.GetAllAsync();
            var eventDtos = _mapper.Map<IEnumerable<EventResponseDto>>(events);
            return Ok(eventDtos);
        }
        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventByIdAsync(int id)
        {
            var eventDetails = await _repository.GetAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventResponseDto>(eventDetails);
            return Ok(eventDto);
        }

        // POST: api/events
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Event data is null");
            }

            // تحويل EventDto إلى Event
            var eventEntity = _mapper.Map<Events>(eventDto);

            // إضافة الحدث إلى قاعدة البيانات
            await _repository.AddAsync(eventEntity);
            await _repository.SaveChangesAsync();

            var eventResponseDto = _mapper.Map<EventResponseDto>(eventEntity);
            return Ok(eventResponseDto); 
        }

        // DELETE: api/events/{id}
        [HttpDelete("{id}")]
       //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _repository.GetAsync(id);
            if (ev == null) return NotFound();

            await _repository.DeleteAsync(ev);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/events/{id}
        [HttpPut("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto updatedEventDto)
        {
            var ev = await _repository.GetAsync(id);
            if (ev == null) return NotFound();

            // تحويل EventDto إلى Entity
            var updatedEvent = _mapper.Map(updatedEventDto, ev); 

           await  _repository.UpdateAsync(updatedEvent);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}

