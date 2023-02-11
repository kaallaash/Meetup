using Meetup.DAL.Entities;
using Meetup.DAL.IntegrationTests.Helpers;

namespace Meetup.DAL.IntegrationTests.Entities;

public static class TestEventEntity
{
    internal static IEnumerable<EventEntity> GetValidEventEntitiesWithId() => new List<EventEntity>()
    {
        EventEntityHelper.CreateValidEventEntity(1),
        EventEntityHelper.CreateValidEventEntity(2),
        EventEntityHelper.CreateValidEventEntity(3),
        EventEntityHelper.CreateValidEventEntity(4),
        EventEntityHelper.CreateValidEventEntity(5)
    };

    internal static IEnumerable<EventEntity> GetValidCreatedEventEntities() => new List<EventEntity>()
    {
        EventEntityHelper.CreateValidEventEntityWithoutId(),
        EventEntityHelper.CreateValidEventEntityWithoutId(),
        EventEntityHelper.CreateValidEventEntityWithoutId(),
        EventEntityHelper.CreateValidEventEntityWithoutId(),
        EventEntityHelper.CreateValidEventEntityWithoutId()
    };

    public static IEnumerable<object[]> GetValidEventEntities()
    {
        foreach (var validCreatedEventEntity in GetValidCreatedEventEntities())
        {
            yield return new object[] { validCreatedEventEntity };
        }
    }
}