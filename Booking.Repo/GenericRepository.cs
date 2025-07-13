using Booking.Core.Models;
using Booking.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Core.Repo.contract;   



namespace Booking.Repo
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EventBookingDbContext _context;

        public GenericRepository(EventBookingDbContext context) 
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
     
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);       
        }

        public async Task UpdateAsync(T entity)
        {
             _context.Update(entity);    
        }

        public async Task DeleteAsync(T entity)
        {

             _context.Remove(entity); 
        }
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
