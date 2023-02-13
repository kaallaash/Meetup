using Meetup.DAL.Entities;

namespace Meetup.API.Tests.Helpers.EntityHelpers;

public static class SpeakerEntityHelper
{
    public static SpeakerEntity CreateValidSpeakerEntity(int id) => new SpeakerEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        RefreshToken = $"RefreshToken{id}",
        RefreshTokenExpiryTime = DateTimeOffset.Now,
        Events = new List<EventEntity>()
        {
            EventEntityHelper.CreateValidEventEntityWithoutSpeaker(id)
        }
    };

    public static SpeakerEntity CreateValidSpeakerEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new SpeakerEntity()
        {
            Name = $"Name{number}",
            Email = $"Email{number}@gmail.com",
            Password = $"password{number}"
        };
    }

    public static SpeakerEntity CreateValidSpeakerEntityWithoutEvent(int id) => new SpeakerEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        RefreshToken = $"RefreshToken{id}",
        RefreshTokenExpiryTime = DateTimeOffset.Now
    };
}