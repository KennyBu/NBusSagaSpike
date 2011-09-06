using NBTY.Core.Containers.Registration;
using Ninject.Modules;

namespace NBTY.Core.Containers.Ninject.Registration
{
    public class ConventionalRegistrationModule : NinjectModule
    {
        public override void Load()
        {
            DependencyRegistration.Items.Scan(assembly_scanner =>
            {
                assembly_scanner.AddAllAssembliesInAppDomain();
                assembly_scanner.ApplyConvention<UsesRegistrationAttributeConvention<RegisterAsRequestScopedAttribute>>();
                assembly_scanner.ApplyConvention<UsesRegistrationAttributeConvention<RegisterAsTransientAttribute>>();
                assembly_scanner.ApplyConvention<UsesRegistrationAttributeConvention<RegisterAsSingletonAttribute>>();
            })
            .ProcessWith(NinjectRegistrationNodeVisitor.RequestNodeVisitor(Kernel))
            .ProcessWith(NinjectRegistrationNodeVisitor.TransientNodeVisitor(Kernel))
            .ProcessWith(NinjectRegistrationNodeVisitor.SingletonNodeVisitor(Kernel));
        }
    }
}