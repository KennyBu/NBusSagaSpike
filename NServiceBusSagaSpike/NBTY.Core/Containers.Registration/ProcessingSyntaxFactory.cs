namespace NBTY.Core.Containers.Registration
{

    public interface IRegistrationProcessingSyntaxFactory
    {
        IRegistrationProcessingSyntax CreateToProcess(IDependencyGraph graph);
    }

    public class RegistrationProcessingSyntaxFactory : IRegistrationProcessingSyntaxFactory
    {
        public IRegistrationProcessingSyntax CreateToProcess(IDependencyGraph graph)
        {
            return new RegistrationProcessingSyntax(graph);
        }
    }
}