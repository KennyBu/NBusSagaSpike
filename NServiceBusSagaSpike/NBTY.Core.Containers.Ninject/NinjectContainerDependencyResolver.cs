using System.Collections.Generic;
using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public delegate TItem KernelResolutionItem<TItem>(IKernel kernel);

    public class NinjectContainerDependencyResolver : IContainerDependencyResolver
    {
        public readonly IKernel _kernel;

        public NinjectContainerDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IEnumerable<TDependency> All<TDependency>()
        {
            return _kernel.GetAll<TDependency>();
        }

        public TDependency An<TDependency>()
        {
            return _kernel.Get<TDependency>();
        }
    }
}