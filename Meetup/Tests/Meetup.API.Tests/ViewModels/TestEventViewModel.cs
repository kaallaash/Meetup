using Meetup.API.Tests.Helpers.ViewModelHelpers;
using Meetup.API.ViewModels;

namespace Meetup.API.Tests.ViewModels;

public static class TestEventViewModel
{    
    public static EventViewModel GetValidEventViewModel =>
        EventViewModelHelper.CreateEventViewModel(1);

    public static EventViewModel GetValidCreateEventViewModel =>
        EventViewModelHelper.CreateEventViewModel(6);

    public static IEnumerable<EventViewModel> GetValidEventViewModels =>
        new List<EventViewModel>()
        {
            EventViewModelHelper.CreateEventViewModel(1),
            EventViewModelHelper.CreateEventViewModel(2),
            EventViewModelHelper.CreateEventViewModel(3),
            EventViewModelHelper.CreateEventViewModel(4),
            EventViewModelHelper.CreateEventViewModel(5),
        };
}