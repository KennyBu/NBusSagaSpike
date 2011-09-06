namespace NBTY.Core.Containers.Registration
{
    public interface IDependencyGraphFactory
    {
        IDependencyGraph CreateGraph();
    }

    public class DependencyGraphFactory : IDependencyGraphFactory
    {
        public IDependencyGraph CreateGraph()
        {
            return new DependencyGraph();
        }
    }
}