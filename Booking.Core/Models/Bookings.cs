using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Models
{
    public  class Bookings:BaseEntity
    {
     public DateTime BookingDate { get; set; }
        public int EventId { get; set; }
        public Events Event { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int NumberOfSeats { get; set; }
        public string BookingStatus { get; set; }  //  "Pending", "Confirmed", "Canceled"

     
    }
}
//Creating a new booking: Initially, the status could be "Pending".

//Confirming a booking: After payment or verification, you could change the status to "Confirmed".

//Canceling a booking: If the user cancels the booking, you could update the status to "Canceled".