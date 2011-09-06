using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public interface IConfigureTheKernel
    {
        void Configure(IKernel kernel);
    }
}