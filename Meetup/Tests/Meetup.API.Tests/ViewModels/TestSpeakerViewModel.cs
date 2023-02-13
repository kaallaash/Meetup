using Meetup.API.Tests.Helpers.ViewModelHelpers;
using Meetup.API.ViewModels;

namespace Meetup.API.Tests.ViewModels;

public static class TestSpeakerViewModel
{
    public static SpeakerViewModel GetValidSpeakerViewModel =>
        SpeakerViewModelHelper.CreateSpeakerViewModel(1);

    public static SpeakerViewModel GetValidCreateSpeakerViewModel =>
        SpeakerViewModelHelper.CreateSpeakerViewModel(6);

    public static IEnumerable<SpeakerViewModel> GetValidSpeakerViewModels =>
        new List<SpeakerViewModel>()
        {
            SpeakerViewModelHelper.CreateSpeakerViewModel(1),
            SpeakerViewModelHelper.CreateSpeakerViewModel(2),
            SpeakerViewModelHelper.CreateSpeakerViewModel(3),
            SpeakerViewModelHelper.CreateSpeakerViewModel(4),
            SpeakerViewModelHelper.CreateSpeakerViewModel(5),
        };
}