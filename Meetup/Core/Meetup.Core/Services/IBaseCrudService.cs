using Meetup.Core.Interfaces.CrudOperations;

namespace Meetup.Core.Services;

public interface IBaseCrudService<T1, in T2> :
    IGetByIdOperation<T1>,
    IGetAllOperation<T1>,
    ICreateOperation<T1>,
    IUpdateOperation<T1>,
    IDeleteOperation<T2>
{ }