﻿namespace Meetup.Core.Interfaces.CrudOperations;

public interface IDeleteOperation<in T>
{
    Task Delete(T value, CancellationToken cancellationToken);
}