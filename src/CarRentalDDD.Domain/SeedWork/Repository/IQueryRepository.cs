using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public interface IQueryRepository<TEntity>
    {
        void AddInclusion(IInclusion<TEntity> inclusion);

        IEnumerable<IInclusion<TEntity>> GetInclusions();

        void AddSpecification(ISpecification<TEntity> specification);

        void AddSpecification(SpecificationType specificationType, ISpecification<TEntity> specification);

        IEnumerable<(SpecificationType?, ISpecification<TEntity>)> GetSpecifications();
        
        Expression<Func<TEntity, bool>> GetSpecificationExpression();


        bool HasSpecifications { get; }
    }
}
