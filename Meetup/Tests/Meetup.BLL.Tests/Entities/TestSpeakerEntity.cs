using Meetup.BLL.Tests.Helpers.EntityHelpers;
using Meetup.DAL.Entities;

namespace Meetup.BLL.Tests.Entities;

public static class TestSpeakerEntity
{
    public static SpeakerEntity GetValidSpeakerEntity => SpeakerEntityHelper.CreateValidSpeakerEntity(1);

    public static IEnumerable<SpeakerEntity> GetValidSpeakerEntities => new List<SpeakerEntity>()
    {
        SpeakerEntityHelper.CreateValidSpeakerEntity(1),
        SpeakerEntityHelper.CreateValidSpeakerEntity(2),
        SpeakerEntityHelper.CreateValidSpeakerEntity(3),
        SpeakerEntityHelper.CreateValidSpeakerEntity(4),
        SpeakerEntityHelper.CreateValidSpeakerEntity(5)
    };
}