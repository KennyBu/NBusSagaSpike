using NBTY.Core.Containers;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection.Expressions
{
    public static class ExpressionGateway
    {
 
        public static IMethodFromExpressionMapper Method()
        {
            //return Container.Resolve().An<IMethodFromExpressionMapper>();
            return new MethodFromExpressionMapper();
        }

        public static IPropertyFromExpressionMapper Property()
        {
            //return Container.Resolve().An<IPropertyFromExpressionMapper>();
            return new PropertyFromExpressionMapper();
        }
    }
}