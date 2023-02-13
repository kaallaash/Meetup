using Meetup.DAL.Entities;

namespace Meetup.BLL.Tests.Helpers.EntityHelpers;

public static class EventEntityHelper
{
    public static EventEntity CreateValidEventEntity(int id) => new EventEntity()
    {
        Id = id,
        Title = $"Title{id}",
        Description = $"Description{id}",
        Location = $"Location{id}",
        Date = DateTime.Now,
        SpeakerId = id,
        Speaker = SpeakerEntityHelper.CreateValidSpeakerEntityWithoutEvent(id)
    };
    public static EventEntity CreateValidEventEntityWithoutSpeaker(int id) =>
        new EventEntity()
        {
            Id = id,
            Title = $"Title{id}",
            Description = $"Description{id}",
            Location = $"Location{id}",
            Date = DateTime.Now,
            SpeakerId = id,
        };
}