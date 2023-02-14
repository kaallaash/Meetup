using Meetup.DAL.Entities;

namespace AuthorizationService.API.Tests.Helpers;

public static class SpeakerEntityHelper
{
    public static SpeakerEntity Create(int id) => new SpeakerEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Email = $"Email{id}",
        Password = $"Password{id}",
    };
}