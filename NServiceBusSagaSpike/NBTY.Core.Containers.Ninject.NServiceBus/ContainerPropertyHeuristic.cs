using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Selection.Heuristics;

namespace NBTY.Core.Containers.Ninject.NServiceBus
{
    public interface IContainerPropertyHeuristic : IInjectionHeuristic
    {
        void Register(Type component);
    }

    public class ContainerPropertyHeuristic : IContainerPropertyHeuristic
    {
        public IList<Type> _registeredTypes = new List<Type>();
        public INinjectSettings Settings { get; set; }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Register(Type component)
        {
            _registeredTypes.Add(component);
        }

        public bool ShouldInject(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;

            return propertyInfo != null && IsInjectable(propertyInfo);
        }

        bool IsInjectable(PropertyInfo property)
        {
            return this._registeredTypes.Any(x => property.DeclaringType.IsAssignableFrom(x))
                && this._registeredTypes.Any(x => property.PropertyType.IsAssignableFrom(x))
                    && property.CanWrite;
        }

        protected virtual void Dispose(bool disposing)
        {
            this._registeredTypes.Clear();
        }
    }
}