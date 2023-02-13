using Meetup.BLL.Models;

namespace Meetup.BLL.Tests.Helpers.ModelHelpers;

public static class SpeakerModelHelper
{
    public static SpeakerModel CreateValidSpeakerModel(int id) => new SpeakerModel()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        RefreshToken = $"RefreshToken{id}",
        RefreshTokenExpiryTime = DateTimeOffset.Now,
        Events = new List<EventModel>()
        {
            EventModelHelper.CreateValidEventModelWithoutSpeaker(id)
        }
    };
    public static SpeakerModel CreateValidSpeakerModelWithoutEvent(int id) => new SpeakerModel()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}@mail.com",
        Password = $"password{id}",
        RefreshToken = $"RefreshToken{id}",
        RefreshTokenExpiryTime = DateTimeOffset.Now
    };

}
