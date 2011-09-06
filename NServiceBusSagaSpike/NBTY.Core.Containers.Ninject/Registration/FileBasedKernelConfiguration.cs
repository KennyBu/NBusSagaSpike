using Ninject;

namespace NBTY.Core.Containers.Ninject.Registration
{
    public class FileBasedKernelConfiguration : IConfigureTheKernel
    {
        public void Configure(IKernel kernel)
        {
            kernel.Load("*.dll".AsEnumerable());
        }
    }
}