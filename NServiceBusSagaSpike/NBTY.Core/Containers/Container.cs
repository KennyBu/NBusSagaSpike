using System;

namespace NBTY.Core.Containers
{
    public class Container
    {
        public static ContainerFacadeResolver FacadeResolver = delegate { throw new NotImplementedException("This needs to be setup by the startup block"); };

        public static IContainerDependencyResolver Resolve()
        {
            return FacadeResolver();
        }
    }
}