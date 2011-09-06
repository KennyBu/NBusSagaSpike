using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NBTY.Core.Containers.Registration
{
    public interface IAssemblyStore
    {
        void Add(Assembly the_assembly);
        void AddAllAssemblies(IEnumerable<Assembly> assemblies);
        IEnumerable<Type> AllTypes();
    }

    public class AssemblyStore : List<Assembly>, IAssemblyStore
    {
        public void AddAllAssemblies(IEnumerable<Assembly> assemblies)
        {
            AddRange(assemblies);
        }

        public IEnumerable<Type> AllTypes()
        {
            return this.SelectMany(assembly => assembly.GetTypes());
        }
    }
}