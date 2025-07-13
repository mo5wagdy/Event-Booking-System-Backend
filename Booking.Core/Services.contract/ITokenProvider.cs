using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Core.Models.Identity;

namespace  Booking.Core.Services.contract   
{
    public  interface ITokenProvider
    {
        Task<string> CreateTokenAsync(AppUser user, IList<string> roles);

    }
}
