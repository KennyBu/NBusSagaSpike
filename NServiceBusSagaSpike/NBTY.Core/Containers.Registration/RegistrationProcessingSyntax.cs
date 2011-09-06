namespace NBTY.Core.Containers.Registration
{
    public interface IRegistrationProcessingSyntax
    {
        IRegistrationProcessingSyntax ProcessWith(IRegistrationNodeVisitor visitor);
    }

    public class RegistrationProcessingSyntax : IRegistrationProcessingSyntax
    {
        IDependencyGraph graph;

        public RegistrationProcessingSyntax(IDependencyGraph graph)
        {
            this.graph = graph;
        }

        public IRegistrationProcessingSyntax ProcessWith(IRegistrationNodeVisitor visitor)
        {
            graph.VisitAllItemsWith(visitor);
            return this;
        }
    }
}