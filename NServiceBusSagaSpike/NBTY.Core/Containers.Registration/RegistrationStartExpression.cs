namespace NBTY.Core.Containers.Registration {
    public interface IRegistrationStartSyntax {
        IRegistrationProcessingSyntax Scan(ConfigureScanner configure);
    }

    public class RegistrationStartSyntax : IRegistrationStartSyntax {
        IAssemblyDependencyScannerFactory _assemblyDependencyScannerFactory;
        IDependencyGraphFactory _dependencyGraphFactory;
        IRegistrationProcessingSyntaxFactory _registrationProcessingSyntaxFactory;

        public RegistrationStartSyntax(IAssemblyDependencyScannerFactory assemblyDependencyScannerFactory,
                                           IDependencyGraphFactory dependencyGraphFactory, IRegistrationProcessingSyntaxFactory registrationProcessingSyntaxFactory) {
            _assemblyDependencyScannerFactory = assemblyDependencyScannerFactory;
            _registrationProcessingSyntaxFactory = registrationProcessingSyntaxFactory;
            _dependencyGraphFactory = dependencyGraphFactory;
        }

        public IRegistrationProcessingSyntax Scan(ConfigureScanner configure) {
            var dependencyScanner = _assemblyDependencyScannerFactory.CreateScanner();
            configure(dependencyScanner);
            var dependencyGraph = _dependencyGraphFactory.CreateGraph();
            dependencyScanner.ApplyTo(dependencyGraph);
            return _registrationProcessingSyntaxFactory.CreateToProcess(dependencyGraph);

        }
    }
}