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
            Expression<Func<TEntity, bool>> expression = _specifications[0].Criteria;

            var parameter = Expression.Parameter(typeof(TEntity), "t");

            for (int i = 1; i < _specifications.Count; i++)
            {
                var leftVisitor = new ReplaceExpressionVisitor(expression.Parameters[0], parameter);
                var left = leftVisitor.Visit(expression.Body);
                var rightVisitor = new ReplaceExpressionVisitor(_specifications[i].Criteria.Parameters[0], parameter);
                var right = rightVisitor.Visit(_specifications[i].Criteria.Body);

                switch (_specificationType[i])
                {
                    case SpecificationType.And:
                        expression = Expression.Lambda<Func<TEntity, bool>>(Expression.And(left, right), parameter);
                        break;

                    case SpecificationType.Or:                        
                        expression = Expression.Lambda<Func<TEntity, bool>>(Expression.Or(left, right), parameter);
                        break;

                    case SpecificationType.AndNot:
                        expression = Expression.Lambda<Func<TEntity, bool>>(Expression.And(left, Expression.Not(right)), parameter);
                        break;

                    case SpecificationType.OrNot:                        
                        expression = Expression.Lambda<Func<TEntity, bool>>(Expression.Or(left, Expression.Not(right)), parameter);
                        break;

                    default:
                        break;
                }
            }
            return expression;
        }



        public bool HasSpecifications => _specifications.Count > 0;
    }


    internal class ReplaceExpressionVisitor
    : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
