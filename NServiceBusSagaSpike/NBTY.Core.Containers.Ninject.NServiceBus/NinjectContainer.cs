using System;
using System.Collections.Generic;
using System.Linq;
using NBTY.Core.Containers.Ninject.NServiceBus.KernelConfiguration;
using NServiceBus.ObjectBuilder;
using NServiceBus.ObjectBuilder.Common;
using Ninject;
using Ninject.Parameters;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public class NinjectContainer : IContainer
    {
        public IKernel _kernel;
        public ICreateAComponentBinder _componentBinderFactory;

        public NinjectContainer(IKernel kernel, ICreateAComponentBinder component_binder_factory)
        {
            this._kernel = kernel;
            _componentBinderFactory = component_binder_factory;
            kernel.Bind<IContainer>().ToConstant(this).InSingletonScope();
        }

        public object Build(Type typeToBuild)
        {
            return this._kernel.Get(typeToBuild);
        }

        public IEnumerable<object> BuildAll(Type typeToBuild)
        {
            return this._kernel.GetAll(typeToBuild);
        }

        public void Configure(Type component, ComponentCallModelEnum callModel)
        {
            if (IsAlreadyConfigured(component)) return;

            _componentBinderFactory.CreateBinder(callModel, component).Run();
        }

        public void ConfigureProperty(Type component, string property, object value)
        {
            var bindings = this._kernel.GetBindings(component).ToList();

            if (bindings.Count ==0) throw new ArgumentException(string.Format("Component of type:{0} + not registered", component.FullName));

            bindings.ForEach(binding => binding.Parameters.Add(new PropertyValue(property, value)));
        }

        public void RegisterSingleton(Type lookupType, object instance)
        {
            this._kernel.Bind(lookupType).ToConstant(instance);
        }

        public bool IsAlreadyConfigured(Type componentType)
        {
            return this._kernel.GetBindings(componentType).Any();
        }
    }
}