using Meetup.API.ViewModels;

namespace Meetup.API.Tests.Helpers.ViewModelHelpers;

public static class SpeakerViewModelHelper
{
    public static SpeakerViewModel CreateSpeakerViewModel(int id) => new SpeakerViewModel()
    {
        Id = id,
        Name = $"Name{id}",
        Events = new List<EventViewModel>()
    };
}