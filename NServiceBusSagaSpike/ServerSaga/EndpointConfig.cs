using NBTY.Core.Containers.Ninject.NServiceBus;
using NBTY.Core.Containers.Ninject.Registration;
using NServiceBus;

namespace ServerSaga
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .Ninject<FileBasedKernelConfiguration>()
                .XmlSerializer(); 
        }
    }
}