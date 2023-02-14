using AuthorizationService.API.Tests.Helpers;
using Meetup.DAL.Entities;

namespace AuthorizationService.API.Tests.Entities;

public static class TestSpeakerEntity
{
    public static IEnumerable<SpeakerEntity> GetValidSpeakerEntities => new List<SpeakerEntity>()
    {
        SpeakerEntityHelper.Create(1),
        SpeakerEntityHelper.Create(2),
        SpeakerEntityHelper.Create(3),
        SpeakerEntityHelper.Create(4),
        SpeakerEntityHelper.Create(5)
    };
}
