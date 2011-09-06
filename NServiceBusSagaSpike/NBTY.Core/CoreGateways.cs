using NBTY.Core.Reflection;

namespace NBTY.Core
{
    public static class CoreGateways
    {
        public static IJsonGateway Json()
        {
            return new JsonGateway();
        }

        public static IReflectionGateway Reflection()
        {
            return new ReflectionGateway();
        }
    }
}