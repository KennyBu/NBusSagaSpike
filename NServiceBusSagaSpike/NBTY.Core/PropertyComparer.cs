using System;
using System.Collections.Generic;

namespace NBTY.Core
{
    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _selector;

        /// <summary>
        /// Creates a new instance of PropertyComparer.
        /// </summary>
        /// <param name="selector">The property on type T 
        /// to perform the comparison on.</param>
        public PropertyComparer(Func<T, object> selector)
        {
            _selector = selector;
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            var xValue = _selector(x);
            var yValue = _selector(y);

            return (xValue == null && yValue == null) || xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            //get the value of the comparison property out of obj
            //object propertyValue = _PropertyInfo.GetValue(obj, null);
            var propertyValue = _selector(obj);

            if (propertyValue == null)
                return 0;

            else
                return propertyValue.GetHashCode();
        }

        #endregion
    }
}