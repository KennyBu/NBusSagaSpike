using System;
using System.Reflection;

namespace NBTY.Core.Reflection
{
    public class PropertyHasAttribute
    {
        Type _attributeToMatch;

        public PropertyHasAttribute(Type attributeToMatch)
        {
            _attributeToMatch = attributeToMatch;
        }

        public bool Matches(ICustomAttributeProvider property)
        {
            return property.GetCustomAttributes(_attributeToMatch, false).Length > 0;
        }
    }
}