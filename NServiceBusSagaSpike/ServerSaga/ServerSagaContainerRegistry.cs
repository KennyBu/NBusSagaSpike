using ServerSaga.TestEntity;
using StructureMap.Configuration.DSL;

namespace ServerSaga
{
    public class ServerSagaContainerRegistry : Registry  
    {
        public ServerSagaContainerRegistry()
        {
            For<IWarrior>().Use<Ninja>();
        }
    }
}