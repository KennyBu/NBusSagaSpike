using System.Reflection;

namespace NBTY.Core.Reflection
{
    public class NestedPropertyDetail : IPropertyDetail
    {
        public IPropertyDetail firstProperty;
        public IPropertyDetail secondProperty;

        public NestedPropertyDetail(IPropertyDetail firstProperty, IPropertyDetail secondProperty)
        {
            this.firstProperty = firstProperty;
            this.secondProperty = secondProperty;
        }

        public void SetValue(object target, object value)
        {
            if (firstProperty.GetValue(target) == null)
            {
                // If the first property is null, attempt first to instantiate it
                firstProperty.SetValue(target, firstProperty.UnderlyingPropertyInfo.PropertyType.Nbty().CreateInstance());
            }
            secondProperty.SetValue(firstProperty.GetValue(target), value);
        }

        public PropertyInfo UnderlyingPropertyInfo
        {
            get { return secondProperty.UnderlyingPropertyInfo; }
        }

        public object GetValue(object target)
        {
            return secondProperty.GetValue(firstProperty.GetValue(target));
        }
    }
}