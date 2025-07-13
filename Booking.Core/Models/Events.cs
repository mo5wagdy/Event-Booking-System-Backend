using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Models
{
    public class Events : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageName { get; set; }  // اسم الصورة المحفوظ في السيرفر
        public int Category { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; } = string.Empty;


        public ICollection<Bookings> Bookings { get; set; }
    }
    }



