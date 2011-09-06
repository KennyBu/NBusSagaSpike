using System;
using System.Reflection;

namespace NBTY.Core.Containers.Registration
{
    public interface IAssemblyDependencyScanner
    {
        void Add(Assembly assemblyToScan);
        void AddAssemblyContaining<TAssemblyType>();
        void ApplyConvention<TTypeScanningConvention>() where TTypeScanningConvention : ITypeRegistrationConvention, new();
        void AddAllAssembliesInAppDomain();
        void ApplyTo(IDependencyGraph dependencyGraph);
    }

    public class AssemblyDepedencyScanner : IAssemblyDependencyScanner
    {
        public IAssemblyStore _assemblyStore;
        public IRegistrationConventions _registrationConventions;

        public AssemblyDepedencyScanner(IAssemblyStore assemblyStore, IRegistrationConventions registrationConventions)
        {
            _assemblyStore = assemblyStore;
            _registrationConventions = registrationConventions;
        }

        public void AddAssemblyContaining<TAssemblyType>()
        {
            Add(typeof(TAssemblyType).Assembly);
        }

        public void Add(Assembly assemblyToScan)
        {
            _assemblyStore.Add(assemblyToScan);
        }

        public void ApplyConvention<TScanningConvention>() where TScanningConvention : ITypeRegistrationConvention, new()
        {
            _registrationConventions.Register(new TScanningConvention());
        }

        public void AddAllAssembliesInAppDomain()
        {
            _assemblyStore.AddAllAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void ApplyTo(IDependencyGraph dependencyGraph)
        {
            _registrationConventions.ApplyTo(dependencyGraph, _assemblyStore.AllTypes());
        }
    }
}