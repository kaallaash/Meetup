using Meetup.DAL.Entities;

namespace Meetup.DAL.IntegrationTests.Helpers;

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

    public static EventEntity CreateValidEventEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new EventEntity()
        {
            Title = $"Title{number}",
            Description = $"Description{number}",
            Location = $"Location{number}",
            Date = DateTime.Now,
            SpeakerId = number,
            Speaker = new SpeakerEntity()
        };
    }

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