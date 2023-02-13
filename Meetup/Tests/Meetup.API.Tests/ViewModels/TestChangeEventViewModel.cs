using Meetup.API.Tests.Helpers.ViewModelHelpers;
using Meetup.API.ViewModels;

namespace Meetup.API.Tests.ViewModels;

public static class TestChangeEventViewModel
{
    public static ChangeEventViewModel GetValidChangeEventViewModel =>
        ChangeEventViewModelHelper.CreateChangeEventViewModel(1);

    public static IEnumerable<ChangeEventViewModel> GetValidChangeEventViewModels =>
        new List<ChangeEventViewModel>()
        {
            ChangeEventViewModelHelper.CreateChangeEventViewModel(1),
            ChangeEventViewModelHelper.CreateChangeEventViewModel(2),
            ChangeEventViewModelHelper.CreateChangeEventViewModel(3),
            ChangeEventViewModelHelper.CreateChangeEventViewModel(4),
            ChangeEventViewModelHelper.CreateChangeEventViewModel(5)
        };
}