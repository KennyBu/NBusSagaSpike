using Ninject;
using Ninject.Selection;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public interface IConfigureCoreBindings : IVisitor<IKernel>
    {
    }


    public class ConfigureCoreBinding : IConfigureCoreBindings
    {
        public void Process(IKernel item)
        {
            item.Bind<IContainerPropertyHeuristic>().To<ContainerPropertyHeuristic>()
                .InSingletonScope()
                .WithPropertyValue(NinjectConstants.SETTINGS_PROPERTY, ctx => ctx.Kernel.Settings);

            item.Components.Get<ISelector>().InjectionHeuristics.Add(
                item.Get<IContainerPropertyHeuristic>());
        }
    }
}