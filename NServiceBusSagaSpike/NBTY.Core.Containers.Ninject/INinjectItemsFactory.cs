using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public interface INinjectItemsFactory
    {
        IKernel CreateKernel();
        IContainerDependencyResolver CreateContainerAdapter(IKernel kernel);
    }
}