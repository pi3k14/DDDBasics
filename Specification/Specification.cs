using System;
using System.Linq;
using System.Linq.Expressions;

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// Class that support DDD Specifiation, but also predicates for use in "native" C# methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Specification<TEntity> : IDeferredSpecification<TEntity>
    {
        /// <summary>
        /// Implicit cast to Expression
        /// </summary>
        /// <param name="specification">An Specification</param>
        /// <returns></returns>
        public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification)
        {
            return specification.Expression;
        }

        /// <summary>
        /// Implicit cast to delegate
        /// </summary>
        /// <param name="specification">An Specification</param>
        /// <returns></returns>
        public static implicit operator Func<TEntity, bool>(Specification<TEntity> specification)
        {
            return specification.Delegate;
        }

        /// <summary>
        /// Implicit cast to Predicate
        /// </summary>
        /// <param name="specification">An Specification</param>
        /// <returns></returns>
        public static implicit operator Predicate<TEntity>(Specification<TEntity> specification)
        {
            return specification.Predicate;
        }

        public static Specification<TEntity> Not(Expression<Func<TEntity, bool>> expression)
        {
            ParameterExpression param = expression.Parameters.Single();

            UnaryExpression exp = Expression<Func<TEntity, bool>>.Not(expression.Body);

            return new Specification<TEntity>(Expression<Func<TEntity, bool>>.Lambda<Func<TEntity, bool>>(exp, param));
        }

        public static Specification<TEntity> And(Expression<Func<TEntity, bool>> expression1, Expression<Func<TEntity, bool>> expression2)
        {
            return BinaryCombine(Expression<Func<TEntity, bool>>.AndAlso, expression1, expression2);
        }

        public static Specification<TEntity> Or(Expression<Func<TEntity, bool>> expression1, Expression<Func<TEntity, bool>> expression2)
        {
            return BinaryCombine(Expression<Func<TEntity, bool>>.OrElse, expression1, expression2);
        }

        private static Specification<TEntity>
#if NET6_0_OR_GREATER
            ? 
#endif
            fAll;
        public static Specification<TEntity> All
        {
            get
            {
                if (fAll == null)
                {
                    fAll = new Specification<TEntity>(_ => true);
                }

                return fAll;
            }
        }

        private static Specification<TEntity>
#if NET6_0_OR_GREATER
            ? 
#endif
            fNone;
        public static Specification<TEntity> None
        {
            get
            {
                if (fNone == null)
                {
                    fNone = new Specification<TEntity>(_ => false);
                }

                return fNone;
            }
        }

        private static Specification<TEntity> BinaryCombine(Func<Expression, Expression, BinaryExpression> func, Expression<Func<TEntity, bool>> expression1, Expression<Func<TEntity, bool>> expression2)
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(TEntity));

            BinaryExpression exp = func(expression1.Body, expression2.Body);
            exp = (BinaryExpression)new ParameterReplacement(param).Visit(exp);

            return new Specification<TEntity>(Expression<Func<TEntity, bool>>.Lambda<Func<TEntity, bool>>(exp, param));
        }

        public Specification(Expression<Func<TEntity, bool>> expression)
        {
            fExpression = expression;
        }

        public Specification<TEntity> Not()
        {
            return Not(Expression);
        }

        public Specification<TEntity> And(Expression<Func<TEntity, bool>> expression)
        {
            return And(Expression, expression);
        }

        public Specification<TEntity> Or(Expression<Func<TEntity, bool>> expression)
        {
            return Or(Expression, expression);
        }

        #region IDeferredSpecification

        private readonly Expression<Func<TEntity, bool>> fExpression;
        public Expression<Func<TEntity, bool>> Expression
        {
            get { return fExpression; }
        }

        private Func<TEntity, bool>
#if NET6_0_OR_GREATER
            ? 
#endif
             fDelegate;
        public Func<TEntity, bool> Delegate
        {
            get
            {
                if (fDelegate == null)
                {
                    fDelegate = Expression.Compile();
                }

                return fDelegate;
            }
        }

        private Predicate<TEntity>
#if NET6_0_OR_GREATER
            ? 
#endif
             fPredicate;
        public Predicate<TEntity> Predicate
        {
            get
            {
                if (fPredicate == null)
                {
                    fPredicate = new Predicate<TEntity>(Delegate);
                }

                return fPredicate;
            }
        }

        #region ISpecification

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Delegate.Invoke(entity);
        }

        #endregion

        #endregion
    }
}