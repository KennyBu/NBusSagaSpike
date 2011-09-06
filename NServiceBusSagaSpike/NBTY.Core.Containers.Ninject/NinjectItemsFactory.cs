using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public class NinjectItemsFactory : INinjectItemsFactory
    {
        public IKernel CreateKernel()
        {
            return new StandardKernel();
        }

        public IContainerDependencyResolver CreateContainerAdapter(IKernel kernel)
        {
            return new NinjectContainerDependencyResolver(kernel);
        }
    }
}