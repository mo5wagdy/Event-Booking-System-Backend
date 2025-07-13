namespace SharedLiberary.DTOS
{
    public class BookingDto
    {
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; }
        public int UserId { get; set; } // For User Which Is booked RN
        public int NumberOfSeats { get; set; }
    }

}
