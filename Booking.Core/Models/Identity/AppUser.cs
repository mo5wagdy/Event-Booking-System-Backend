using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Booking.Core.Models.Identity
{
    public  class AppUser:IdentityUser<int>

    {
        public string DisplayName {  get; set; }
      
    }
}
