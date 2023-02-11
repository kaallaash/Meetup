using Meetup.Core.Interfaces.CrudOperations;

namespace Meetup.Core.Repositories;

public interface IBaseCrudRepository<T> :
    IGetByIdOperation<T>,
    IGetAllOperation<T>,
    ICreateOperation<T>,
    IUpdateOperation<T>,
    IDeleteOperation<T>
{ }