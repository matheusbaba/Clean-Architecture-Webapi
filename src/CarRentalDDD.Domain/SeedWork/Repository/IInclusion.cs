using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public interface IInclusion<T>
    {
        Expression<Func<T, object>> Expression { get; }
    }
}
