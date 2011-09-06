using System;
using NBTY.Core.Containers.Registration;
using NServiceBus.ObjectBuilder;
using Ninject;

namespace NBTY.Core.Containers.Ninject.NServiceBus.KernelConfiguration
{
    public interface ICreateAComponentBinder
    {
        IBindAComponent CreateBinder(ComponentCallModelEnum callModel, Type componentToBind);
    }

    [RegisterAsSingleton(typeof(ICreateAComponentBinder))]
    public class ComponentBinderFactory : ICreateAComponentBinder
    {
        IKernel _kernel;
        public ICallModelMapper _callModelMapper = new CallModelMapper();

        public ComponentBinderFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IBindAComponent CreateBinder(ComponentCallModelEnum callModel, Type componentToBind)
        {
            return new BindAComponent(_callModelMapper.GetScopePolicyFor(callModel),
                                      componentToBind, _kernel, _kernel.Get<IContainerPropertyHeuristic>());
        }
    }
}