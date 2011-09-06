using System;
using NBTY.Core.Containers.Ninject.NServiceBus.KernelConfiguration;
using NServiceBus.ObjectBuilder.Common;
using Ninject;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public class NinjectContainerFactory
    {
        public IKernelConfigurators _kernelConfigurators = new KernelConfigurators(); 

        public Func<IKernel, ICreateAComponentBinder> _binderFactoryProvider =
            (kernel) => new ComponentBinderFactory(kernel);

        public IContainer CreateContainerFor(IKernel kernel)
        {
            _kernelConfigurators.ForEach(x => x.Process(kernel));
            return new NinjectContainer(kernel, _binderFactoryProvider(kernel));
        }
    }
}