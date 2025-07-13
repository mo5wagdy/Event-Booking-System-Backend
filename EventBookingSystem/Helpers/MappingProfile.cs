using AutoMapper;
using SharedLiberary.DTOS;
using Booking.Core.Models;

namespace EventBookingSystem.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking.Core.Models.Bookings, BookingDto>();

            CreateMap<Events, EventDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageName))
                .ReverseMap()
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Events, EventResponseDto>(); // Assuming this still needed
        }
    }
}
