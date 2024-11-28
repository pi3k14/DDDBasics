using System;
using System.Linq.Expressions;

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// Extension of the Specification pattern to better support the C# programming model (delegates, lambda, linq)
    /// </summary>
    /// <typeparam name="TEntity">Generic type for the object to be validated</typeparam>
    public interface IDeferredSpecification<TEntity> : ISpecification<TEntity>
    {
        /// <summary>
        /// Property that returns the expression tree for the specification (allowing EF Core/IQueryable to run this server side)
        /// </summary>
        Expression<Func<TEntity, bool>> Expression { get; }

        /// <summary>
        /// Property that returns the delegate for the specification
        /// </summary>
        Func<TEntity, bool> Delegate { get; }

        /// <summary>
        /// Property that returns the Predicate for the specification (for use by Array.Find etc)
        /// </summary>
        Predicate<TEntity> Predicate { get; }
    }
}
