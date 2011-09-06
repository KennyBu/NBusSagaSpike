using System;
using NBTY.Core.Containers;

namespace NBTY.Core.Reflection
{
    public static class TypeExtensions
    {
        public static ITypeGateway Nbty(this Type type)
        {
            return Container.Resolve().An<TypeGatewayFactory>().Invoke(type);
        }
    }
}
