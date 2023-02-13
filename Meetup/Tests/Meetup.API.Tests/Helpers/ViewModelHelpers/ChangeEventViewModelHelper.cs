using Meetup.API.ViewModels;

namespace Meetup.API.Tests.Helpers.ViewModelHelpers;

public static class ChangeEventViewModelHelper
{
    public static ChangeEventViewModel CreateChangeEventViewModel(int id) => new ChangeEventViewModel()
    {
        Title = $"Title{id}",
        Description = $"Description{id}",
        Location = $"Location{id}",
        Date = DateTimeOffset.Now.AddDays(1),
        SpeakerId = id
    };
}