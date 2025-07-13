using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLiberary.DTOS
{
    public  class BookingResponseDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfSeats { get; set; }
        public string BookingStatus { get; set; }

        public BEventDto Event { get; set; }
        public BUserDto User { get; set; }
    }
}
