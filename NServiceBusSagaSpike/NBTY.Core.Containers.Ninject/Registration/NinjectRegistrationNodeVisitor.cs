using System;
using NBTY.Core.Containers.Registration;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Syntax;

namespace NBTY.Core.Containers.Ninject.Registration
{
    public delegate void NinjectLifecycleConfiguration(IBindingWhenInNamedWithOrOnSyntax<object> binding);

    public class NinjectRegistrationNodeVisitor : IRegistrationNodeVisitor
    {
        IKernel _kernel;
        Func<IContext, object> _scope;

        public NinjectRegistrationNodeVisitor(IKernel kernel, Func<IContext, object> scope)
        {
            _kernel = kernel;
            _scope = scope;
        }

        public void Process(IRegistrationNode item)
        {
            _kernel.Bind(item.InterfaceType).To(item.ImplementationType).InScope(_scope);
        }

        public static IRegistrationNodeVisitor SingletonNodeVisitor(IKernel kernel)
        {
            return CreateVisitor<SingletonRegistrationNode>(kernel, StandardScopeCallbacks.Singleton);
        }

        public static IRegistrationNodeVisitor RequestNodeVisitor(IKernel kernel)
        {
            return CreateVisitor<RequestRegistrationNode>(kernel, StandardScopeCallbacks.Request);
        }

        public static IRegistrationNodeVisitor TransientNodeVisitor(IKernel kernel)
        {
            return CreateVisitor<TransientRegistrationNode>(kernel, StandardScopeCallbacks.Transient);
        }

        static IRegistrationNodeVisitor CreateVisitor<TNode>(IKernel kernel, Func<IContext, object> scopeCallback)
        {
            return new NodeSpecificRegistrationNodeVisitor<TNode>(new
                                                                      NinjectRegistrationNodeVisitor(kernel,
                                                                                                     scopeCallback));
        }
    }
}