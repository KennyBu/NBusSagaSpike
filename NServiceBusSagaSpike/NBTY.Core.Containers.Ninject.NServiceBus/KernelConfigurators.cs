using System.Collections;
using System.Collections.Generic;
using NBTY.Core.Containers.Ninject.NServiceBus.KernelConfiguration;
using Ninject;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public interface IKernelConfigurators : IEnumerable<IVisitor<IKernel>>
    {
        
    }
    public class KernelConfigurators : IKernelConfigurators
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IVisitor<IKernel>> GetEnumerator()
        {
            yield return new ConfigureCoreBinding();
            yield return new ConfigureActivationStrategies();
        }
    }
}