using Meetup.Core.Repositories;

namespace Meetup.DAL.Interfaces.Repositories;

public interface ISpeakerRepository<T1> : IBaseCrudRepository<T1>
{
    Task<T1?> GetByEmail(string email, CancellationToken cancellationToken);
}