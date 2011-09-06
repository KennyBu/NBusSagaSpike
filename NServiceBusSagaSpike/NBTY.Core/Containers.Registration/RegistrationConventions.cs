using System;
using System.Collections.Generic;

namespace NBTY.Core.Containers.Registration
{
    public interface IRegistrationConventions
    {
        void Register(ITypeRegistrationConvention convention);
        void ApplyTo(IDependencyGraph graph, IEnumerable<Type> allTypes);
    }

    public class RegistrationConventions : List<ITypeRegistrationConvention>, IRegistrationConventions
    {
        public void Register(ITypeRegistrationConvention convention)
        {
            Add(convention);
        }

        public void ApplyTo(IDependencyGraph graph, IEnumerable<Type> allTypes)
        {
            allTypes.ForEach(potential_type_to_register => ForEach(convention => convention.ApplyTo(potential_type_to_register, graph)));
        }
    }
}