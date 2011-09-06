using System;

namespace NBTY.Core.Containers.Registration
{
    public interface ITypeRegistrationConvention 
    {
        void ApplyTo(Type serviceType, IDependencyGraph graph);
    }
}