using System.Collections.Generic;

namespace NBTY.Core.Reflection.Expressions
{
    public interface IMethodParameterDetail
    {
        KeyValuePair<string, object> CreatePair();
    }

    public class MethodParameterDetail : IMethodParameterDetail
    {
        private readonly string key;
        private readonly object value;

        public MethodParameterDetail(string key, object value)
        {
            this.key = key;
            this.value = value;
        }

        public KeyValuePair<string, object> CreatePair()
        {
            return new KeyValuePair<string, object>(key, value);
        }
    }
}