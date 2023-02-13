using AutoMapper;
using Meetup.BLL.Models;
using Meetup.DAL.Entities;

namespace Meetup.BLL.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventEntity, EventModel>().ReverseMap();
        CreateMap<SpeakerEntity, SpeakerModel>().ReverseMap();
    }
}
