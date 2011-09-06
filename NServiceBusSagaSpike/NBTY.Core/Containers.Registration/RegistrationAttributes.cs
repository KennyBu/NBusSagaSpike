using System;

namespace NBTY.Core.Containers.Registration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public abstract class ContainerRegistrationAttribute : Attribute
    {
        public Type _interfaceToRegisterAs { get; private set; }
        public Type _implementationToRegisterAs { get; set; }

        public ContainerRegistrationAttribute(Type interfaceToRegisterAs)
        {
            _interfaceToRegisterAs = interfaceToRegisterAs;
        }

        public abstract IRegistrationNode CreateRegistrationNode();
    }

    public class RegisterAsTransientAttribute : ContainerRegistrationAttribute
    {
        public RegisterAsTransientAttribute(Type interfaceToRegisterAs) : base(interfaceToRegisterAs)
        {
        }

        public override IRegistrationNode CreateRegistrationNode()
        {
            return new TransientRegistrationNode(_interfaceToRegisterAs, _implementationToRegisterAs);
        }
    }

    public class RegisterAsSingletonAttribute : ContainerRegistrationAttribute
    {
        public RegisterAsSingletonAttribute(Type interfaceToRegisterAs) : base(interfaceToRegisterAs)
        {
        }

        public override IRegistrationNode CreateRegistrationNode()
        {
            return new SingletonRegistrationNode(_interfaceToRegisterAs, _implementationToRegisterAs);
        }
    }

    public class RegisterAsRequestScopedAttribute : ContainerRegistrationAttribute
    {
        public RegisterAsRequestScopedAttribute(Type interfaceToRegisterAs) : base(interfaceToRegisterAs)
        {
        }

        public override IRegistrationNode CreateRegistrationNode()
        {
            return new RequestRegistrationNode(_interfaceToRegisterAs, _implementationToRegisterAs);
        }
    }
}