using System;

namespace NBTY.Core.Containers.Registration
{
    public class TransientRegistrationNode : RegistrationNode
    {
        public TransientRegistrationNode(Type interfaceType, Type implementationType) : base(interfaceType, implementationType)
        {
        }
    }
    public class SingletonRegistrationNode : RegistrationNode
    {
        public SingletonRegistrationNode(Type interfaceType, Type implementationType) : base(interfaceType, implementationType)
        {
        }
    }

    public class RequestRegistrationNode : RegistrationNode
    {
        public RequestRegistrationNode(Type interfaceType, Type implementationType) : base(interfaceType, implementationType)
        {
        }
    }
}