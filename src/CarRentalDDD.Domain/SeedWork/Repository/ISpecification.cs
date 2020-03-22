using System;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        //ISpecification<T> And(ISpecification<T> specification);
        //ISpecification<T> Or(ISpecification<T> specification);
        //ISpecification<T> AndNot(ISpecification<T> specification);
        //ISpecification<T> OrNot(ISpecification<T> specification);
    }
}
