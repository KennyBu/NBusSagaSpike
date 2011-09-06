using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NBTY.Core.Reflection.Expressions;

namespace NBTY.Core.Reflection
{
    public interface ITypeGateway
    {
        object GetPropertyValue(object target, string propertyName);
        void SetPropertyValue(object target, string propertyName, object value);
        Type GetPropertyType(string propertyName);
        IEnumerable<string> GetNamesOfPropertiesWithAttribute<TAttribute>() where TAttribute : Attribute;
        string GetNameOfPropertyWithAttribute<TAttribute>() where TAttribute : Attribute;
        IEnumerable<object> GetValuesOfPropertiesWithAttribute<TAttribute>(object target) where TAttribute : Attribute;
        object GetValueOfPropertyWithAttribute<TAttribute>(object target) where TAttribute : Attribute;
        object CallMethod(object target, string methodName, IEnumerable<object> parameters);
        object CreateInstance();
        object CallGenericMethod(object target, string methodName, IEnumerable<object> parameters, params Type[] genericParameterTypes);
        object CreateGenericInstance(params Type[] genericParameterTypes);
        bool ImplementsGenericType(Type genericType);
    }

    public class TypeGateway : ITypeGateway
    {
        readonly Type _type;
        readonly IPropertyFactory _propertyFactory;
        readonly IMethodMapper _methodMapper;

        public TypeGateway(Type type, IPropertyFactory propertyFactory, IMethodMapper methodMapper)
        {
            _type = type;
            _propertyFactory = propertyFactory;
            _methodMapper = methodMapper;
        }

        public object GetPropertyValue(object target, string propertyName)
        {
            return _propertyFactory.CreatePropertyToAccess(_type, propertyName).GetValue(target);
        }

        public void SetPropertyValue(object target, string propertyName, object value)
        {
            _propertyFactory.CreatePropertyToAccess(_type, propertyName).SetValue(target,
                                                                                  value);
        }

        public Type GetPropertyType(string propertyName)
        {
            return
                _propertyFactory.CreatePropertyToAccess(_type, propertyName).
                    UnderlyingPropertyInfo.PropertyType;
        }

        public IEnumerable<string> GetNamesOfPropertiesWithAttribute<TAttribute>() where TAttribute : Attribute
        {
            return _type.GetProperties().Where(p => p.HasAttribute<TAttribute>()).Select(p => p.Name);
        }

        public string GetNameOfPropertyWithAttribute<TAttribute>() where TAttribute : Attribute
        {
            return GetNamesOfPropertiesWithAttribute<TAttribute>().SingleOrDefault();
        }

        public IEnumerable<object> GetValuesOfPropertiesWithAttribute<TAttribute>(object target) where TAttribute : Attribute
        {
            return GetNamesOfPropertiesWithAttribute<TAttribute>().Select(p => _type.GetProperty(p).GetValue(target, null));
        }

        public object GetValueOfPropertyWithAttribute<TAttribute>(object target) where TAttribute : Attribute
        {
            return GetValuesOfPropertiesWithAttribute<TAttribute>(target).SingleOrDefault();
        }

        public object CallMethod(object target, string methodName, IEnumerable<object> parameters)
        {
            return _methodMapper.MapFrom(_type, methodName).Invoke(target, parameters);
        }

        /// <summary>
        /// Attempt to generate an instance of the type. 
        /// If an error occurs (for ex. if there is no parameterless constructor) return null
        /// </summary>
        /// <returns></returns>
        public object CreateInstance()
        {
            try
            {
                return Activator.CreateInstance(_type);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object CallGenericMethod(object target, string methodName, IEnumerable<object> parameters, params Type[] genericParameterTypes)
        {
            return _methodMapper.MapFrom(_type, methodName).MakeGeneric(genericParameterTypes).Invoke(target, parameters);
        }

        /// <summary>
        /// For ex. type is typeof(Collection<>) and genericParameterTypes is typeof(T)
        /// </summary>
        /// <param name="genericParameterTypes"></param>
        /// <returns></returns>
        public object CreateGenericInstance(params Type[] genericParameterTypes)
        {
            if (!_type.IsGenericTypeDefinition) return null;
            var typeToCreate = _type.MakeGenericType(genericParameterTypes);
            return Activator.CreateInstance(typeToCreate);
        }

        public bool ImplementsGenericType(Type genericType)
        {
            return _type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
        }
    }
}