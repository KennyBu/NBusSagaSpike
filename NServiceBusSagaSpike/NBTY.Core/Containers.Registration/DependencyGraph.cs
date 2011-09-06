using System.Collections.Generic;

namespace NBTY.Core.Containers.Registration
{
    public interface IDependencyGraph : IEnumerable<IRegistrationNode>
    {
        void Register(IRegistrationNode node);
    }

    public class DependencyGraph : List<IRegistrationNode>, IDependencyGraph
    {
        public void Register(IRegistrationNode node)
        {
            Add(node);
        }
    }
}