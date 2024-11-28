// ParameterReplacement, an idea after reading http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx (ParameterRebinder)

using System.Linq.Expressions;

namespace Kodefabrikken.DDD.Specification
{
    /// <summary>
    /// A class for replacing the parameter in an unary expression
    /// </summary>
    internal class ParameterReplacement : ExpressionVisitor
    {
        private readonly ParameterExpression fParameter;

        /// <summary>
        /// Creates the replacement visitor
        /// </summary>
        /// <param name="parameter">new parameter to replace old with</param>
        public ParameterReplacement(ParameterExpression parameter)
        {
            fParameter = parameter;
        }

        /// <summary>
        /// Overload to replace the parameter
        /// </summary>
        /// <param name="node">visited node</param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(fParameter);
        }
    }
}
