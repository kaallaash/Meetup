using AuthorizationService.API.ViewModels;
using AutoMapper;
using Meetup.BLL.Models;

namespace AuthorizationService.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginViewModel, SpeakerModel>().ReverseMap();
    }
}