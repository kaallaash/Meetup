using AutoMapper;
using Meetup.BLL.Models;
using Meetup.DAL.Entities;

namespace Meetup.BLL.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventModel, EventEntity>();
        CreateMap<EventEntity, EventModel>()
            .ForMember(m => m.Speaker, opt =>
                opt.MapFrom(e => new SpeakerModel()
                {
                    Id = e.Speaker.Id,
                    Name = e.Speaker.Name
                }));
        CreateMap<SpeakerModel, SpeakerEntity>().ReverseMap();
    }
}
