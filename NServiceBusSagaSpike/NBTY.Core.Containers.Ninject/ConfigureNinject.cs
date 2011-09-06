using Ninject;

namespace NBTY.Core.Containers.Ninject
{
    public class ConfigureNinject
    {
        INinjectItemsFactory _ninjectItemFactoryFactory;
        IConfigureTheKernel _kernel_configurator;

        public ConfigureNinject(INinjectItemsFactory ninjectItemFactoryFactory,
                                IConfigureTheKernel kernelConfigurator)
        {
            _ninjectItemFactoryFactory = ninjectItemFactoryFactory;
            _kernel_configurator = kernelConfigurator;
        }

        public IKernel Run()
        {
            var kernel = _ninjectItemFactoryFactory.CreateKernel();
            var containerAdapter = _ninjectItemFactoryFactory.CreateContainerAdapter(kernel);

            kernel.Bind<IKernel>().ToConstant(kernel);
            kernel.Bind<IContainerDependencyResolver>().ToConstant(containerAdapter);
            Container.FacadeResolver = () => containerAdapter;

            _kernel_configurator.Configure(kernel);

            return kernel;
        }

        public static IKernel StartKernelUsing<TConfigurator>() where TConfigurator :IConfigureTheKernel,new()
        {
            return new ConfigureNinject(new NinjectItemsFactory(), new TConfigurator()).Run();
        }
    }
}