//using NBTY.Core.Containers.Ninject.NServiceBus;
//using NBTY.Core.Containers.Ninject.Registration;
using NServiceBus;
using StructureMap;

//using NServiceBus.ObjectBuilder.Ninject.Config;

//using NServiceBus.ObjectBuilder.Ninject.Config;

namespace ServerSaga
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ServerSagaContainerRegistry>();
            });

            
            
            
            
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
                
                .Log4Net()
                
                //.DefaultBuilder()
                .StructureMapBuilder(ObjectFactory.Container)
                //.Ninject<FileBasedKernelConfiguration>()
               //.CastleWindsorBuilder()
                //.NinjectBuilder()
                
                
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