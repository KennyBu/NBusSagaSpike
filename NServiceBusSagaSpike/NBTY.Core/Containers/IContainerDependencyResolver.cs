using System.Collections.Generic;

namespace NBTY.Core.Containers
{
    public interface IContainerDependencyResolver
    {
        TDependency An<TDependency>();
        IEnumerable<TDependency> All<TDependency>();
    }
}