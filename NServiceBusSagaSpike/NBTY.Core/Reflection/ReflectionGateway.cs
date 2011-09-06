using System;
using System.Linq;
using System.Linq.Expressions;
using NBTY.Core.Reflection.Expressions;

namespace NBTY.Core.Reflection
{
    public interface IReflectionGateway
    {
        object CallGenericMethod<T>(T target, Expression<Action<T>> methodExpression, params Type[] genericParameterTypes);
    }

    public class ReflectionGateway : IReflectionGateway
    {
        readonly IMethodFromExpressionMapper _methodFromExpressionMapper;
        readonly IMethodMapper _methodMapper;

        //public ReflectionGateway(IMethodFromExpressionMapper methodFromExpressionMapper, IMethodMapper methodMapper)
        public ReflectionGateway()
        {
            _methodFromExpressionMapper = new MethodFromExpressionMapper();
            _methodMapper = new MethodMapper();
        }

        public object CallGenericMethod<T>(T target, Expression<Action<T>> methodExpression, params Type[] genericParameterTypes)
        {
            var methodName = _methodFromExpressionMapper.MapNameFrom(methodExpression);
            var parameters = _methodFromExpressionMapper.MapParametersFrom(methodExpression).Select(x => x.CreatePair().Value);

            return _methodMapper.MapFrom(typeof(T), methodName).MakeGeneric(genericParameterTypes).Invoke(target, parameters);
        }
    }
}