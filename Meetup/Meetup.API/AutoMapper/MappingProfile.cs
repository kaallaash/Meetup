using AutoMapper;
using Meetup.API.ViewModels;
using Meetup.BLL.Models;

namespace Meetup.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SpeakerViewModel, SpeakerModel>().ReverseMap();
        CreateMap<ChangeSpeakerViewModel, SpeakerModel>().ReverseMap();
        CreateMap<EventViewModel, EventModel>().ReverseMap();
        CreateMap<ChangeEventViewModel, EventModel>().ReverseMap();
    }
}