// ParameterBinder, an idea after reading http://stackoverflow.com/a/12863851

using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// A class for binding values to parameters
    /// </summary>
    internal class ParameterBinder : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, ConstantExpression> fParameter;
        /// <summary>
        /// Creates the parameterbinder visitor
        /// </summary>
        /// <param name="parameter">a dictionary with parameters to bind to constants</param>
        public ParameterBinder(IDictionary<ParameterExpression, ConstantExpression> parameter)
        {
            fParameter = parameter;
        }

        /// <summary>
        /// Overload of parameter visitor
        /// </summary>
        /// <param name="node">parameter to check if should be bound</param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (fParameter.TryGetValue(node, out ConstantExpression
#if NET6_0_OR_GREATER
                ? 
#endif
                constant))
            {
                return base.VisitConstant(constant);
            }

            return base.VisitParameter(node);
        }
    }
}
