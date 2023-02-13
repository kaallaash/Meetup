using Meetup.Core.Services;

namespace Meetup.BLL.Interfaces;

public interface IEventService<T1, in T2> : IBaseCrudService<T1, T2>
{
}