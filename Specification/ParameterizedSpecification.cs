using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// A class for Specification with parameter that are bound right before usage, not at definition
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="Arg">Argument to lambda</typeparam>
    public class ParameterizedSpecification<TEntity, Arg>
    {
        private readonly Expression<Func<TEntity, Arg, bool>> fPredicateExpression;
        /// <summary>
        /// Creation of unary Specification
        /// </summary>
        /// <param name="predicateExpression">Specification lambda</param>
        public ParameterizedSpecification(Expression<Func<TEntity, Arg, bool>> predicateExpression)
        {
            fPredicateExpression = predicateExpression;
        }

        /// <summary>
        /// Bind argument of Specification
        /// </summary>
        /// <param name="arg">Value to bind to argument</param>
        /// <returns></returns>
        public Specification<TEntity> Bind(Arg arg)
        {
            ParameterExpression firstparam = fPredicateExpression.Parameters[0];

            Dictionary<ParameterExpression, ConstantExpression> parameter = new Dictionary<ParameterExpression, ConstantExpression>()
            {
                { fPredicateExpression.Parameters[1], Expression.Constant(arg) }
            };

            Expression exp = new ParameterBinder(parameter).Visit(fPredicateExpression.Body);

            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(exp, firstparam));
        }
    }

    public class ParameterizedSpecification<TEntity, Arg1, Arg2>
    {
        private readonly Expression<Func<TEntity, Arg1, Arg2, bool>> fPredicateExpression;

        public ParameterizedSpecification(Expression<Func<TEntity, Arg1, Arg2, bool>> predicateExpression)
        {
            fPredicateExpression = predicateExpression;
        }

        public Specification<TEntity> Bind(Arg1 arg1, Arg2 arg2)
        {
            ParameterExpression firstparam = fPredicateExpression.Parameters[0];

            Dictionary<ParameterExpression, ConstantExpression> parameter
                = new Dictionary<ParameterExpression, ConstantExpression>()
            {
                {fPredicateExpression.Parameters[1], Expression.Constant(arg1) },
                {fPredicateExpression.Parameters[2], Expression.Constant(arg2) }
            };

            Expression exp = new ParameterBinder(parameter).Visit(fPredicateExpression.Body);

            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(exp, firstparam));
        }
    }

    public class ParameterizedSpecification<TEntity, Arg1, Arg2, Arg3>
    {
        private readonly Expression<Func<TEntity, Arg1, Arg2, Arg3, bool>> fPredicateExpression;

        public ParameterizedSpecification(Expression<Func<TEntity, Arg1, Arg2, Arg3, bool>> predicateExpression)
        {
            fPredicateExpression = predicateExpression;
        }

        public Specification<TEntity> Bind(Arg1 arg1, Arg2 arg2, Arg3 arg3)
        {
            ParameterExpression firstparam = fPredicateExpression.Parameters[0];

            Dictionary<ParameterExpression, ConstantExpression> parameter
                = new Dictionary<ParameterExpression, ConstantExpression>()
            {
                {fPredicateExpression.Parameters[1], Expression.Constant(arg1) },
                {fPredicateExpression.Parameters[2], Expression.Constant(arg2) },
                {fPredicateExpression.Parameters[3], Expression.Constant(arg3) }
            };

            Expression exp = new ParameterBinder(parameter).Visit(fPredicateExpression.Body);

            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(exp, firstparam));
        }
    }

    public class ParameterizedSpecification<TEntity, Arg1, Arg2, Arg3, Arg4>
    {
        private readonly Expression<Func<TEntity, Arg1, Arg2, Arg3, Arg4, bool>> fPredicateExpression;

        public ParameterizedSpecification(Expression<Func<TEntity, Arg1, Arg2, Arg3, Arg4, bool>> predicateExpression)
        {
            fPredicateExpression = predicateExpression;
        }

        public Specification<TEntity> Bind(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4)
        {
            ParameterExpression firstparam = fPredicateExpression.Parameters[0];

            Dictionary<ParameterExpression, ConstantExpression> parameter
                = new Dictionary<ParameterExpression, ConstantExpression>()
            {
                {fPredicateExpression.Parameters[1], Expression.Constant(arg1) },
                {fPredicateExpression.Parameters[2], Expression.Constant(arg2) },
                {fPredicateExpression.Parameters[3], Expression.Constant(arg3) },
                {fPredicateExpression.Parameters[4], Expression.Constant(arg4) }
            };

            Expression exp = new ParameterBinder(parameter).Visit(fPredicateExpression.Body);

            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(exp, firstparam));
        }
    }
}