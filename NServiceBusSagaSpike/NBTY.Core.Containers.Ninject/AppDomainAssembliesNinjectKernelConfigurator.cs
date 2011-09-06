using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public delegate IEnumerable<Assembly> AssembliesToInspectForStartup();

    public class AppDomainAssembliesNinjectKernelConfigurator : IConfigureTheKernel
    {
        public static AssembliesToInspectForStartup assemblies = AppDomain.CurrentDomain.GetAssemblies;

        public void Configure(IKernel kernel)
        {
            kernel.Load(assemblies());
        }
    }
}