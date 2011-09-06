using System;

namespace NBTY.Core.Reflection
{
    public class PropertyMissingOnTypeException : Exception
    {
        private const string EXCEPTION_MESSAGE_FORMAT = "The type {0} does not have a property named: {1}";


        public string PropertyName { get; private set; }

        public PropertyMissingOnTypeException(string propertyName, Type typeWithoutProperty):base(string.Format(EXCEPTION_MESSAGE_FORMAT,typeWithoutProperty.Name,propertyName))
        {
            PropertyName = propertyName;
            TypeWithoutProperty = typeWithoutProperty;
        }

        public Type TypeWithoutProperty { get; set; }
    }
}