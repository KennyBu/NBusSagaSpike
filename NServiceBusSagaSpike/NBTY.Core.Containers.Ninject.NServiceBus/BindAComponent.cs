using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Activation;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public interface IBindAComponent : ICommand
    {
    }

    public class BindAComponent : IBindAComponent
    {
        public Func<IContext, object> _scope;
        public Type _typeToBind;
        public IKernel _kernel;
        public IContainerPropertyHeuristic _propertyHeuristic;

        public BindAComponent(Func<IContext, object> scope, Type typeToBind, IKernel kernel,
                              IContainerPropertyHeuristic propertyHeuristic)
        {
            _scope = scope;
            _typeToBind = typeToBind;
            _kernel = kernel;
            _propertyHeuristic = propertyHeuristic;
        }

        public void Run()
        {
            this.BindComponentToItself(_typeToBind, _scope);
            this.BindAllOfTheInterfacesOnTheComponent(_typeToBind, _scope);
            this._propertyHeuristic.Register(_typeToBind);
        }

        void BindAllOfTheInterfacesOnTheComponent(Type component, Func<IContext, object> scope)
        {
            var contracts = GetAllTheInterfacesOfTheComponent(component);
            contracts.ForEach(contract => _kernel.Bind(contract).ToMethod(context => context.Kernel.Get(component)).InScope(_scope));
        }

        static IEnumerable<Type> GetAllTheInterfacesOfTheComponent(Type component)
        {
            return component.GetInterfaces().Where(contract => contract != component);
        }

        void BindComponentToItself(Type component, Func<IContext, object> scope)
        {
            this._kernel.Bind(component).ToSelf()
                .InScope(scope);
        }
    }
}