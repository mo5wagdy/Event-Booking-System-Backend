using Booking.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repo
{
    public  class BookingIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>

    {
        public BookingIdentityDbContext(DbContextOptions<BookingIdentityDbContext> options) : base(options)
        {
        
        }
    }
   
    
}
