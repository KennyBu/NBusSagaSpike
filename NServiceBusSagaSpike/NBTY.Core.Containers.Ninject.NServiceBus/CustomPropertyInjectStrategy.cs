using System.Collections.Generic;
using Ninject.Activation;
using Ninject.Activation.Strategies;
using Ninject.Infrastructure;
using Ninject.Injection;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public class CustomPropertyInjectStrategy : PropertyInjectionStrategy
    {
        HashSet<object> activatedInstances = new HashSet<object>();

        public CustomPropertyInjectStrategy(IInjectorFactory injectorFactory)
            : base(injectorFactory)
        {
        }

        public override void Activate(IContext context, InstanceReference reference)
        {
            if (this.activatedInstances.Contains(reference.Instance))
            {
                return;
            }

            if (context.Binding.ScopeCallback != StandardScopeCallbacks.Transient)
            {
                this.activatedInstances.Add(reference.Instance);
            }

            base.Activate(context, reference);
        }

        public override void Deactivate(IContext context, InstanceReference reference)
        {
            this.activatedInstances.Remove(reference.Instance);
            base.Deactivate(context, reference);
        }
    }
}