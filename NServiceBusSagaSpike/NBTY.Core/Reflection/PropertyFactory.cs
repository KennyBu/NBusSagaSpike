using System;
using System.Reflection;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection
{
    public interface IPropertyFactory
    {
        IPropertyDetail CreatePropertyToAccess(Type root, string propertyName);
    }

    [RegisterAsSingleton(typeof(IPropertyFactory))]
    public class PropertyFactory : IPropertyFactory
    {
        readonly IPropertyMapper _propertyMapper;

        //public PropertyFactory(IPropertyMapper propertyMapper)
        public PropertyFactory()
        {
            //_propertyMapper = propertyMapper;
            _propertyMapper = new PropertyMapper();
        }

        public IPropertyDetail CreatePropertyToAccess(Type root, string propertyName)
        {
            IPropertyDetail combined = new NulloProperty();
            var targetType = root;

            propertyName.Split('.').ForEach(currentName =>
            {
                var property = GetPropertyOnType(targetType, currentName);
                combined = new NestedPropertyDetail(combined, _propertyMapper.MapFrom(property));
                targetType = property.PropertyType;
            });

            return combined;
        }

        PropertyInfo GetPropertyOnType(Type targetType, string currentName)
        {
            var property = targetType.GetProperty(currentName);
            if (property != null) return property;

            throw new PropertyMissingOnTypeException(currentName, targetType);
        }

        public class NulloProperty : IPropertyDetail
        {
            public object GetValue(object target)
            {
                return target;
            }

            public void SetValue(object target, object value)
            {
                throw new NotImplementedException();
            }

            public PropertyInfo UnderlyingPropertyInfo
            {
                get { throw new NotImplementedException("You should not be accessing the property on a NulloProperty"); }
            }
        }
    }
}