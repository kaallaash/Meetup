using Meetup.API.Tests.Helpers.ViewModelHelpers;
using Meetup.API.ViewModels;

namespace Meetup.API.Tests.ViewModels;

public static class TestChangeSpeakerViewModel
{
    public static ChangeSpeakerViewModel GetValidChangeSpeakerViewModel =>
        ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(1);

    public static IEnumerable<ChangeSpeakerViewModel> GetValidChangeSpeakerViewModels =>
        new List<ChangeSpeakerViewModel>()
        {
            ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(1),
            ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(2),
            ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(3),
            ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(4),
            ChangeSpeakerViewModelHelper.CreateChangeSpeakerViewModel(5)
        };
}