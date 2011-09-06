using System;

namespace NBTY.Core.Containers.Registration
{
    public interface IRegistrationNode
    {
        Type InterfaceType { get; }
        Type ImplementationType { get; }
    }

    public class RegistrationNode : IRegistrationNode
    {
        public Type InterfaceType { get; private set; }
        public Type ImplementationType { get; private set; }

        public RegistrationNode(Type interfaceType, Type implementationType)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
        }
    }
}