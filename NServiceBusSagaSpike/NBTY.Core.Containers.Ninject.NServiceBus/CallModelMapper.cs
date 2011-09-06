using System;
using System.Collections.Generic;
using NServiceBus.ObjectBuilder;
using Ninject.Activation;
using Ninject.Infrastructure;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{

    public interface ICallModelMapper
    {
         Func<IContext ,object> GetScopePolicyFor(ComponentCallModelEnum callModel);
    }

    public class CallModelMapper : ICallModelMapper
    {
        IDictionary<ComponentCallModelEnum, Func<IContext, object>>  scopeModels = new Dictionary
            <ComponentCallModelEnum, Func<IContext, object>> 
        {
            {ComponentCallModelEnum.Singleton, StandardScopeCallbacks.Singleton},
            {ComponentCallModelEnum.Singlecall, StandardScopeCallbacks.Transient}
        };

        public Func<IContext,object> GetScopePolicyFor(ComponentCallModelEnum callModel)
        {
            EnsureScopePolicyExistsFor(callModel);
            return scopeModels[callModel];
        }

        void EnsureScopePolicyExistsFor(ComponentCallModelEnum callModel)
        {
            if (scopeModels.ContainsKey(callModel)) return;
            throw new ArgumentException("The requested scope model is not supported");
        }
    }
}