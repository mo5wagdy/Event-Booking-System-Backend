using AdminDashBoard.Models;
using AutoMapper;
using Booking.Core.Models;

namespace AdminDashBoard.Helper
{
    public class MapsProfile:Profile
    {
           
            public MapsProfile()
            {
            CreateMap<EventViewModel, Events>();
            //.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Events, EventViewModel>();
                   // . ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<EventViewModel, Events>();

        }
    }

    }
