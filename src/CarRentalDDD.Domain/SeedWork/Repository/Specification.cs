using System;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public class Specification<T> : ISpecification<T>
    {

        //private Expression<Func<T, bool>> _expression;
        //public Expression<Func<T, bool>> ToExpression => _expression;

        //public Specification(Expression<Func<T, bool>> expression)
        //{
        //    _expression = expression;
        //}


        //public ISpecification<T> And(ISpecification<T> specification)
        //{
        //    var parameter = Expression.Parameter(typeof(T));            
        //    BinaryExpression binaryExpression = Expression.And(_expression.Body, specification.ToExpression.Body);
        //    _expression = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
        //    return this;
        //}

        //public ISpecification<T> Or(ISpecification<T> specification)
        //{
        //    var parameter = Expression.Parameter(typeof(T));
        //    BinaryExpression binaryExpression = Expression.Or(_expression.Body, specification.ToExpression.Body);
        //    _expression = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
        //    return this;
        //}
        //public ISpecification<T> AndNot(ISpecification<T> specification)
        //{
        //    var parameter = Expression.Parameter(typeof(T));
        //    BinaryExpression binaryExpression = Expression.And(_expression.Body, Expression.Not(specification.ToExpression.Body));
        //    _expression = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
        //    return this;
        //}
        //public ISpecification<T> OrNot(ISpecification<T> specification)
        //{
        //    var parameter = Expression.Parameter(typeof(T));
        //    BinaryExpression binaryExpression = Expression.Or(_expression.Body, Expression.Not(specification.ToExpression.Body));
        //    _expression = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
        //    return this;
        //}
        public Expression<Func<T, bool>> Criteria { get; }


        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

    }

}
