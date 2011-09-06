using System.Reflection;

namespace NBTY.Core.Reflection
{
    public interface IPropertyDetail
    {
        object GetValue(object target);
        void SetValue(object target, object value);
        PropertyInfo UnderlyingPropertyInfo { get; }
    }

    public class PropertyDetail : IPropertyDetail
    {
        public PropertyInfo UnderlyingPropertyInfo { get; private set; }

        public PropertyDetail(PropertyInfo realProperty)
        {
            this.UnderlyingPropertyInfo = realProperty;
        }

        public void SetValue(object target, object value)
        {
            if(target == null) return;
            if (UnderlyingPropertyInfo != null)
            {
                UnderlyingPropertyInfo.SetValue(target, value, new object[0]);
            }
        }

        public object GetValue(object target)
        {
            if (target == null || UnderlyingPropertyInfo == null) return null;
            return UnderlyingPropertyInfo.GetValue(target, new object[0]);
        }
    }
}