using System;
using System.Reflection;
using NBTY.Core.Containers.Registration;

namespace NBTY.Core.Reflection
{
    public interface IPropertyMapper
    {
        IPropertyDetail MapFrom(PropertyInfo propertyInfo);
        IPropertyDetail MapFrom(Type type, string propertyName);
    }

    [RegisterAsTransient(typeof(IPropertyMapper))]
    public class PropertyMapper : IPropertyMapper
    {
        public IPropertyDetail MapFrom(PropertyInfo propertyInfo)
        {
            return new PropertyDetail(propertyInfo);
        }

        public IPropertyDetail MapFrom(Type type, string propertyName)
        {
            return MapFrom(type.GetProperty(propertyName));
        }
    }
}