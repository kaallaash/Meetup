using Meetup.BLL.Tests.Helpers.EntityHelpers;
using Meetup.DAL.Entities;

namespace Meetup.BLL.Tests.Entities;

public static class TestEventEntity
{
    public static EventEntity GetValidEventEntity => EventEntityHelper.CreateValidEventEntity(1);

    public static IEnumerable<EventEntity> GetValidEventEntities => new List<EventEntity>()
    {
        EventEntityHelper.CreateValidEventEntity(1),
        EventEntityHelper.CreateValidEventEntity(2),
        EventEntityHelper.CreateValidEventEntity(3),
        EventEntityHelper.CreateValidEventEntity(4),
        EventEntityHelper.CreateValidEventEntity(5)
    };
}