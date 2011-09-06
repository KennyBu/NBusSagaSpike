namespace NBTY.Core.Containers.Registration
{
    public interface IAssemblyDependencyScannerFactory
    {
        IAssemblyDependencyScanner CreateScanner();
    }

    public class AssemblyDependencyScannerFactory : IAssemblyDependencyScannerFactory
    {
        public IAssemblyDependencyScanner CreateScanner()
        {
            return new AssemblyDepedencyScanner(new AssemblyStore(), new RegistrationConventions());
        }
    }
}