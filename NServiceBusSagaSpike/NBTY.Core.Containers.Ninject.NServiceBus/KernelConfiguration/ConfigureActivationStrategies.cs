using System.Linq;
using Ninject;
using Ninject.Activation;
using Ninject.Activation.Strategies;
using Ninject.Injection;

namespace NBTY.Core.Containers.Ninject.NServiceBus.KernelConfiguration
{
    public interface IConfigureActivationStrategies : IVisitor<IKernel>
    {
    }

    public class ConfigureActivationStrategies : IConfigureActivationStrategies
    {
        public void Process(IKernel item)
        {
            item.Bind<IInjectorFactory>().ToMethod(ctx => ctx.Kernel.Components.Get<IInjectorFactory>());

            item.Bind<CustomPropertyInjectStrategy>().ToSelf()
                .InSingletonScope()
                .WithPropertyValue(NinjectConstants.SETTINGS_PROPERTY, ctx => ctx.Kernel.Settings);

            UpdateTheActivationStrategiesInThePipeline(item);
        }

        static void UpdateTheActivationStrategiesInThePipeline(IKernel item)
        {
            var currentActivationStrategies = item.Components.Get<IPipeline>().Strategies;

            var newSetOfStrategies = currentActivationStrategies.Where(IsNotAPropertyInjectionStrategy)
                .Union(item.Get<CustomPropertyInjectStrategy>().AsEnumerable())
                .ToList();


            currentActivationStrategies.Clear();
            newSetOfStrategies.ForEach(currentActivationStrategies.Add);
        }

        static bool IsNotAPropertyInjectionStrategy(IActivationStrategy strategy)
        {
            return !strategy.GetType().Equals(typeof(PropertyInjectionStrategy));
        }
    }
}