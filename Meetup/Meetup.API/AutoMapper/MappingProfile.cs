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
        //CreateMap<EventViewModel, EventModel>();
        CreateMap<ChangeEventViewModel, EventModel>().ReverseMap();
        //CreateMap<EventModel, EventViewModel>()
        //    .ForMember(evm => evm.Speaker, opt =>
        //        opt.MapFrom(em => new SpeakerViewModel()
        //        {
        //            Id = em.Speaker.Id,
        //            Name = em.Speaker.Name
        //        }));
        CreateMap<EventViewModel, EventModel>().ReverseMap();
    }
}