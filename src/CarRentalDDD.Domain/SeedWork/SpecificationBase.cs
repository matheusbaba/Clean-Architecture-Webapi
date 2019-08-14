using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {        
        public Expression<Func<T, bool>> Expression { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


        public SpecificationBase(Expression<Func<T, bool>> expression)
        {
            this.Expression = expression;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

   
    }
}
