using Meetup.API.ViewModels;

namespace Meetup.API.Tests.Helpers.ViewModelHelpers;

public static class ChangeSpeakerViewModelHelper
{
    public static ChangeSpeakerViewModel CreateChangeSpeakerViewModel(int id) => new ChangeSpeakerViewModel()
    {
        Name = $"Name{id}",
        Email = $"Email{id}@gmail.com",
        Password = $"Password{id}",
    };
}