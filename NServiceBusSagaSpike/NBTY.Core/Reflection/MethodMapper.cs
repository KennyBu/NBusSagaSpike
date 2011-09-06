using System;
using System.Reflection;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection
{
    public interface IMethodMapper
    {
        IMethodDetail MapFrom(MethodInfo methodInfo);
        IMethodDetail MapFrom(Type type, string methodName);
    }

    [RegisterAsTransient(typeof(IMethodMapper))]
    public class MethodMapper : IMethodMapper
    {
        public IMethodDetail MapFrom(MethodInfo methodInfo)
        {
            return new MethodDetail(methodInfo);
        }

        public IMethodDetail MapFrom(Type type, string methodName)
        {
            return MapFrom(type.GetMethod(methodName));
        }
    }
}