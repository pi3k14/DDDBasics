// ISpecification from Domain-Driven Design by Eric Evans, chapter 10 Supple Design, page 274 : ISBN 0-321-12521-5

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// A slightly modified interface definition from Domain-Driven Design by Eric Evans
    /// </summary>
    /// <typeparam name="TEntity">Generic type for object to be validated</typeparam>
    public interface ISpecification<TEntity>
    {
        /// <summary>
        /// Check if parameter satisfy the specification(requirement)
        /// </summary>
        /// <param name="entity">Object to be validated</param>
        /// <returns><see langword="true"/> - if object satisfy condition</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}
