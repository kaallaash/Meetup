using Meetup.API.Tests.Helpers.EntityHelpers;
using Meetup.DAL.Entities;

namespace Meetup.API.Tests.Entities;

internal static class TestSpeakerEntity
{
    internal static IEnumerable<SpeakerEntity> GetValidSpeakerEntitiesWithId() => new List<SpeakerEntity>()
    {
        SpeakerEntityHelper.CreateValidSpeakerEntity(1),
        SpeakerEntityHelper.CreateValidSpeakerEntity(2),
        SpeakerEntityHelper.CreateValidSpeakerEntity(3),
        SpeakerEntityHelper.CreateValidSpeakerEntity(4),
        SpeakerEntityHelper.CreateValidSpeakerEntity(5)
    };

    internal static IEnumerable<SpeakerEntity> GetValidCreatedSpeakerEntities() => new List<SpeakerEntity>()
    {
        SpeakerEntityHelper.CreateValidSpeakerEntityWithoutId(),
        SpeakerEntityHelper.CreateValidSpeakerEntityWithoutId(),
        SpeakerEntityHelper.CreateValidSpeakerEntityWithoutId(),
        SpeakerEntityHelper.CreateValidSpeakerEntityWithoutId(),
        SpeakerEntityHelper.CreateValidSpeakerEntityWithoutId()
    };

    public static IEnumerable<object[]> GetValidSpeakerEntities()
    {
        foreach (var validCreatedSpeakerEntity in GetValidCreatedSpeakerEntities())
        {
            yield return new object[] { validCreatedSpeakerEntity };
        }
    }
}