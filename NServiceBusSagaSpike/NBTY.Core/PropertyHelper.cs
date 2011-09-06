using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NBTY.Core
{
    /// <summary>
    /// Entity Property helper methods
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// Get an entity's property name given its expression tree.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="type"></param>
        /// <param name="propertyRefExpr"></param>
        /// <example>
        /// string propertyName = PropertyHelper.GetName\<Company\> (c => c.Name);
        /// or
        /// Company company = new Company();
        /// string propertyName = company.GetPropertyName (c => c.Name);
        /// </example>
        /// <returns></returns>
        public static string GetPropertyName<TObject>(this TObject type,
                                                      Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        private static string GetPropertyNameCore(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            var memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                var unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member.Name;

            throw new ArgumentException("No property reference expression was found.",
                                        "propertyRefExpr");
        }
    }
}