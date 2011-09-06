using System;
using System.Linq;

namespace NBTY.Core.Containers.Registration
{
    public class UsesRegistrationAttributeConvention<TAttribute> : ITypeRegistrationConvention
        where TAttribute : ContainerRegistrationAttribute
    {
        public void ApplyTo(Type serviceType, IDependencyGraph graph)
        {
            var raw_attribute = serviceType.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
            if (raw_attribute == null) return;

            var attribute = (TAttribute) raw_attribute;
            attribute._implementationToRegisterAs = serviceType;

            graph.Register(attribute.CreateRegistrationNode());
        }
    }
}