using Meetup.Core.Services;

namespace Meetup.BLL.Interfaces;

public interface ISpeakerService<T1, in T2> : IBaseCrudService<T1, T2>
{
    Task<T1?> GetByEmail(string email,
        CancellationToken cancellationToken);
}