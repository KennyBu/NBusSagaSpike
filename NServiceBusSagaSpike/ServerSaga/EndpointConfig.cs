using NBTY.Core.Containers.Ninject.NServiceBus;
using NBTY.Core.Containers.Ninject.Registration;
using NServiceBus;

namespace ServerSaga
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                /*
                .Ninject<FileBasedKernelConfiguration>()
                //.Sagas()
                //.NHibernateSagaPersisterWithSQLiteAndAutomaticSchemaGeneration()
                .XmlSerializer()
                .UnicastBus()
                .ImpersonateSender(false)
                .LoadMessageHandlers();
                 */

                //.Ninject<FileBasedKernelConfiguration>()
                .Log4Net()
                .DefaultBuilder()
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(false)
                .UnicastBus()
                .ImpersonateSender(false)
                .LoadMessageHandlers();




        }
    }
}