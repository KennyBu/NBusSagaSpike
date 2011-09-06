using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection.Expressions
{
    public interface IMethodFromExpressionMapper
    {
        string MapNameFrom<T>(Expression<Action<T>> methodExpression);
        IEnumerable<IMethodParameterDetail> MapParametersFrom<T>(Expression<Action<T>> methodExpression);

        object InvokeUsingGenericParameters<T>(T target, Expression<Action<T>> methodExpression,
                                               params Type[] genericParameterTypes);
    }

    [RegisterAsSingleton(typeof(IMethodFromExpressionMapper))]
    public class MethodFromExpressionMapper : IMethodFromExpressionMapper
    {
        readonly TypeGatewayFactory _typeGatewayFactory;

        //public MethodFromExpressionMapper(TypeGatewayFactory typeGatewayFactory)
        public MethodFromExpressionMapper()
        {
            //_typeGatewayFactory = typeGatewayFactory;
            _typeGatewayFactory = type => new TypeGateway(type, new PropertyFactory(), new MethodMapper());
        }

        public string MapNameFrom<T>(Expression<Action<T>> methodExpression)
        {
            return ((MethodCallExpression) methodExpression.Body).Method.Name;
        }

        public IEnumerable<IMethodParameterDetail> MapParametersFrom<T>(Expression<Action<T>> methodExpression)
        {
            var methodInfo = (MethodCallExpression) methodExpression.Body;
            var parameters = methodInfo.Method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var argumentExpression = methodInfo.Arguments[i];
                var lambda = Expression.Lambda(Expression.Convert(argumentExpression, argumentExpression.Type));
                var value = lambda.Compile().DynamicInvoke();
                yield return new MethodParameterDetail(parameters[i].Name, value);
            }
        }

        public object InvokeUsingGenericParameters<T>(T target, Expression<Action<T>> methodExpression,
                                                      params Type[] genericParameterTypes)
        {
            var methodName = MapNameFrom(methodExpression);
            var parameters = MapParametersFrom(methodExpression).Select(x => x.CreatePair().Value);

            return _typeGatewayFactory.Invoke(typeof(T)).CallGenericMethod(target, methodName, parameters,
                                                                           genericParameterTypes);
        }
    }
}