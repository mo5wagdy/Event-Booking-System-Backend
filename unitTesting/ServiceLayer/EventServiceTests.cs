using Booking.Core.Models;
using Booking.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Core.Repo.contract;   
using Booking.Repo;
using Microsoft.EntityFrameworkCore.InMemory;

namespace unitTesting.ServiceLayer
{
    public class GenericRepositoryTests
    {
        private readonly EventBookingDbContext _context;
        private readonly GenericRepository<Events> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EventBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique per test run
                .Options;

            _context = new EventBookingDbContext(options);
            _repository = new GenericRepository<Events>(_context);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEvent_WhenCalled()
        {
            // Arrange
            var eventEntity = new Events
            {
                Id = 1,
                Name = "Original Event",
                Description = "Old description"
            };

            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();

            // Update the event
            eventEntity.Name = "Updated Event";
            eventEntity.Description = "New description";

            // Act
            await _repository.UpdateAsync(eventEntity);
            await _repository.SaveChangesAsync();

            // Assert
            var updatedEvent = await _context.Events.FindAsync(1);
            Assert.Equal("Updated Event", updatedEvent.Name);
            Assert.Equal("New description", updatedEvent.Description);
        }
    }


}



