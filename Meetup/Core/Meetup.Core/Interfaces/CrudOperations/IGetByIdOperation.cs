namespace Meetup.Core.Interfaces.CrudOperations;

public interface IGetByIdOperation<T>
{
    Task<T?> GetById(int id,
        CancellationToken cancellationToken);
}