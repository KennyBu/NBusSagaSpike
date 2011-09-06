using System;
using System.Linq.Expressions;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection.Expressions
{
    public interface IPropertyFromExpressionMapper
    {
        /// <summary>
        /// From a property expressed as an expression, return the property name
        /// Ex 1: if TTarget is Customer and expression is c => c.Name return "Name"
        /// Ex 2: if TTarget is Customer and expression is t => t.Address.City return "Address.City"
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        string MapNameFrom<TTarget, TReturnType>(Expression<PropertyAccessor<TTarget, TReturnType>> expression);

        /// <summary>
        /// From a property expressed as a string, return an expression pointing to that property.
        /// Ex 1: if TTarget is Customer and propertyName is "Name" return t => t.Name
        /// Ex 2: if TTarget is Customer and propertyName is "Address.City" return t => t.Address.City
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        Expression<Func<TTarget, object>> MapExpressionFromName<TTarget>(string propertyName);
    }

    [RegisterAsSingleton(typeof(IPropertyFromExpressionMapper))]
    public class PropertyFromExpressionMapper : IPropertyFromExpressionMapper
    {

        public string MapNameFrom<TTarget, TReturnType>(Expression<PropertyAccessor<TTarget, TReturnType>> expression)
        {
            return MapNameFromMemberExpression(expression.Body as MemberExpression);
        }

        public Expression<Func<TTarget, object>> MapExpressionFromName<TTarget>(string propertyName)
        {
            ParameterExpression param = Expression.Parameter(typeof(TTarget), "t");

            Expression bodyExpression = param;
            Type propertyType = typeof (TTarget);

            propertyName.Split('.').ForEach(currentName =>
                {
                    bodyExpression = Expression.Property(bodyExpression, propertyType.GetProperty(currentName));
                    propertyType = propertyType.GetProperty(currentName).PropertyType;
                }
                );

            var lambda = Expression.Lambda<Func<TTarget, object>>(
                    bodyExpression,
                    new ParameterExpression[] { param });

            return lambda;
        }

        private static string MapNameFromMemberExpression(MemberExpression memberExpression)
        {
            string name = "";
            if (memberExpression == null) return name;
            if (memberExpression.Expression as MemberExpression != null)
            {
                name += MapNameFromMemberExpression(memberExpression.Expression as MemberExpression) + ".";
            }
            name += memberExpression.Member.Name;
            return name;
        }

    }
}