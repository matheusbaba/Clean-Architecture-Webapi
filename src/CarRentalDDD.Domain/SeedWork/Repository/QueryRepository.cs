using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public class QueryRepository<TEntity> : IQueryRepository<TEntity>
    {

        #region private fields
        private ICollection<IInclusion<TEntity>> _inclusions = new List<IInclusion<TEntity>>();
        private IList<ISpecification<TEntity>> _specifications = new List<ISpecification<TEntity>>();
        private IList<SpecificationType?> _specificationType = new List<SpecificationType?>();
        #endregion end private fields


        /// <summary>
        /// Get all inclusions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IInclusion<TEntity>> GetInclusions() => _inclusions;


        /// <summary>
        /// Get all specifications
        /// </summary>
        /// <returns>Returns a tuple with specification and type. First speficiation should have type null</returns>
        public IEnumerable<(SpecificationType?, ISpecification<TEntity>)> GetSpecifications()
        {
            for (int i = 0; i < _specifications.Count; i++)
                yield return (_specificationType[i], _specifications[i]);
        }


        /// <summary>
        /// Add an object to be returned in query
        /// </summary>
        /// <param name="inclusion">Object</param>
        public void AddInclusion(IInclusion<TEntity> inclusion)
        {
            _inclusions.Add(inclusion);
        }


        /// <summary>
        /// Add speficiation to filter query
        /// </summary>
        /// <param name="specification">Specification</param>
        public void AddSpecification(ISpecification<TEntity> specification)
        {
            if (_specificationType.Count > 0)
                throw new Exception("This method should be used to add first Specification in QueryRepository only. Please use AddSpecification(SpecificationType specificationType, ISpecification<T> specification) to add more specifications");

            // clean lists as this method doesnt require SpecificationType and is used to add first specification to QueryRepository only
            _specificationType.Clear();
            _specifications.Clear();

            _specificationType.Add(null);
            _specifications.Add(specification);
        }

        /// <summary>
        ///  Add speficiation to filter query
        /// </summary>
        /// <param name="specificationType">Type</param>
        /// <param name="specification">Specification</param>
        public void AddSpecification(SpecificationType specificationType, ISpecification<TEntity> specification)
        {
            if (_specificationType.Count == 0)
                throw new Exception("Please, to add first Specification to QueryRepository, use AddSpecification(ISpecification<T> specification)");
            _specificationType.Add(specificationType);
            _specifications.Add(specification);
        }


        /// <summary>
        /// Returns all specifications in expression format
        /// </summary>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> GetSpecificationExpression()
        {
            Expression<Func<TEntity, bool>> expression = _specifications[0].ToExpression;
            var parameter = Expression.Parameter(typeof(TEntity));

            BinaryExpression binaryExpressionTemp;
            for (int i = 1; i < _specifications.Count; i++)
            {
                switch (_specificationType[i])
                {
                    case SpecificationType.And:                        
                        binaryExpressionTemp = Expression.And(expression.Body, _specifications[i].ToExpression.Body);
                        expression = Expression.Lambda<Func<TEntity, bool>>(binaryExpressionTemp, parameter);
                        break;

                    case SpecificationType.Or:
                        binaryExpressionTemp = Expression.Or(expression.Body, _specifications[i].ToExpression.Body);
                        expression = Expression.Lambda<Func<TEntity, bool>>(binaryExpressionTemp, parameter);
                        break;

                    case SpecificationType.AndNot:
                        binaryExpressionTemp = Expression.And(expression.Body, Expression.Not(_specifications[i].ToExpression.Body));
                        expression = Expression.Lambda<Func<TEntity, bool>>(binaryExpressionTemp, parameter);
                        break;

                    case SpecificationType.OrNot:
                        binaryExpressionTemp = Expression.Or(expression.Body, Expression.Not(_specifications[i].ToExpression.Body));
                        expression = Expression.Lambda<Func<TEntity, bool>>(binaryExpressionTemp, parameter);
                        break;

                    default:
                        break;
                }
            }
            return expression;
        }



        public bool HasSpecifications => _specifications.Count > 0;
    }
}
