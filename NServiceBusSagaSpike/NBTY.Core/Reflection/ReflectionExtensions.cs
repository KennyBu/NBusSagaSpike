using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NBTY.Core.Reflection
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider property)
        {
            return new PropertyHasAttribute(typeof(TAttribute)).Matches(property);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider property) where TAttribute : Attribute
        {
            return property.GetAttributes<TAttribute>().FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider property) where TAttribute : Attribute
        {
            var attributes = property.GetCustomAttributes(typeof(TAttribute), true);
            return attributes.Cast<TAttribute>();
        }

    }
}