namespace NBTY.Core.Containers.Registration
{
    public delegate IRegistrationStartSyntax RegistrationStartExpressionFactory();

    public class DependencyRegistration
    {
        public static RegistrationStartExpressionFactory factory = () =>
        {
            return new RegistrationStartSyntax(new AssemblyDependencyScannerFactory(), new DependencyGraphFactory(),
                new RegistrationProcessingSyntaxFactory());
        };

        public static IRegistrationStartSyntax Items
        {
            get { return factory(); }
        }
    }
}