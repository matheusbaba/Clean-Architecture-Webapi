using System;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public class Inclusion<T> : IInclusion<T>
    {
        //private IList<Expression<Func<T, object>>> _inclusions = new List<Expression<Func<T, object>>>();

        //public IEnumerable<Expression<Func<T, object>>> Inclusions => _inclusions;

        //public Inclusion(Expression<Func<T, object>> expression)
        //{
        //    this._inclusions.Add(expression);
        //}

        //public IInclusion<T> And(IInclusion<T> include)
        //{
        //    var i = include.Inclusions.FirstOrDefault();
        //    if (i != null) _inclusions.Add(i);
        //    return this;
        //}
        public Expression<Func<T, object>> Expression { get; }


        public Inclusion(Expression<Func<T, object>> include)
        {
            Expression = include;
        }
    }
}
