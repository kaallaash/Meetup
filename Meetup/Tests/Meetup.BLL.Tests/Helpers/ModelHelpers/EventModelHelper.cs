using Meetup.BLL.Models;

namespace Meetup.BLL.Tests.Helpers.ModelHelpers;

public static class EventModelHelper
{
    public static EventModel CreateValidEventModel(int id) => new EventModel()
    {
        Id = id,
        Title = $"Title{id}",
        Description = $"Description{id}",
        Location = $"Location{id}",
        Date = DateTime.Now,
        SpeakerId = id,
        Speaker = SpeakerModelHelper.CreateValidSpeakerModelWithoutEvent(id)
    };
    public static EventModel CreateValidEventModelWithoutSpeaker(int id) =>
        new EventModel()
        {
            Id = id,
            Title = $"Title{id}",
            Description = $"Description{id}",
            Location = $"Location{id}",
            Date = DateTime.Now,
            SpeakerId = id,
        };
}