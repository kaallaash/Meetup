using Meetup.API.ViewModels;

namespace Meetup.API.Tests.Helpers.ViewModelHelpers;

public static class EventViewModelHelper
{
    public static EventViewModel CreateEventViewModel(int id) => new EventViewModel()
    {
        Id = id,
        Title = $"Title{id}",
        Description = $"Description{id}",
        Location = $"Location{id}",
        Date = DateTimeOffset.Now.AddDays(1),
        Speaker = new SpeakerViewModel()
    };
}