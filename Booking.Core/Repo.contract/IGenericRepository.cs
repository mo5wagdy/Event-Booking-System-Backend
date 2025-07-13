using Booking.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Booking.Core.Repo.contract
{
    public  interface IGenericRepository <T> where T : BaseEntity

    {
      Task<IReadOnlyList<T>>GetAllAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T entity);    
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<int> CompleteAsync();
    }
}
