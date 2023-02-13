using Meetup.DAL.Entities;

namespace Meetup.BLL.Tests.Helpers.EntityHelpers;

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