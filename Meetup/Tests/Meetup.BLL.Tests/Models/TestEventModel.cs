using Meetup.BLL.Models;
using Meetup.BLL.Tests.Helpers.ModelHelpers;

namespace Meetup.BLL.Tests.Models;

public static class TestEventModel
{
    public static EventModel GetValidEventModel => EventModelHelper.CreateValidEventModel(1);

    public static IEnumerable<EventModel> GetValidEventModels => new List<EventModel>()
    {
        EventModelHelper.CreateValidEventModel(1),
        EventModelHelper.CreateValidEventModel(2),
        EventModelHelper.CreateValidEventModel(3),
        EventModelHelper.CreateValidEventModel(4),
        EventModelHelper.CreateValidEventModel(5)
    };
}