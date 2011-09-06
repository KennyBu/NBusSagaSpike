using System;
using NServiceBus;
using NServiceBus.ObjectBuilder.Common;
using NServiceBus.ObjectBuilder.Common.Config;
using Ninject;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public static class NinjectNServiceBusConfigurationExtensions
    {
        public static Func<IKernel,IContainer> _containerFactory = new NinjectContainerFactory().CreateContainerFor;
        public static Func<Configure, IContainer,Configure> _configurationBlock = (config,container) =>
        {
            ConfigureCommon.With(config, container);
            return config;
        };

        public static Configure Ninject(this Configure config, IKernel kernel)
        {
            return _configurationBlock(config, _containerFactory(kernel));
        }

        public static Configure Ninject<TKernelConfiguration>(this Configure config)
            where TKernelConfiguration : IConfigureTheKernel, new()
        {
            return Ninject(config,ConfigureNinject.StartKernelUsing<TKernelConfiguration>());
        }
    }
}