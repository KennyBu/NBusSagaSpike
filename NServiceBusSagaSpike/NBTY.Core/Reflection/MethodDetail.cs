using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NBTY.Core.Reflection
{
    public interface IMethodDetail
    {
        IMethodDetail MakeGeneric(params Type[] genericParameterTypes);
        object Invoke(object target, IEnumerable<object> parameters);
    }

    public class MethodDetail : IMethodDetail
    {
        MethodInfo _underlyingMethodInfo;

        public MethodDetail(MethodInfo realMethod)
        {
            _underlyingMethodInfo = realMethod;
        }

        public IMethodDetail MakeGeneric(params Type[] genericParameterTypes)
        {
            _underlyingMethodInfo = _underlyingMethodInfo.MakeGenericMethod(genericParameterTypes);
            return this;
        }

        public object Invoke(object target, IEnumerable<object> parameters)
        {
            var parameterArray = (parameters == null) ? null : parameters.ToArray();
            return _underlyingMethodInfo.Invoke(target, parameterArray);
        }
    }
}