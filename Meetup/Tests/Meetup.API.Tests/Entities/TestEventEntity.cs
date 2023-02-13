using Meetup.API.Tests.Helpers.EntityHelpers;
using Meetup.DAL.Entities;

namespace Meetup.API.Tests.Entities;

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
}