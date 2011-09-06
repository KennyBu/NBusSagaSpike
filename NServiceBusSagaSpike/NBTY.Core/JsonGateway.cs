using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceStack.Text;
using ServiceStack.Text.Common;

namespace NBTY.Core
{
    public interface IJsonGateway
    {
        string JsonSerialize<T>(IEnumerable<T> typedList);
        T JsonDeserialize<T>(string data);
        ICollection<T> JsonDeserializeToCollection<T>(string data);
        string FormatToJsonDate(string funkyDateString);
        string JsonSerialize<T>(T item);
        string FormatToJsonWithProperDateFormatting<T>(string json);
    }

    public class JsonGateway : IJsonGateway
    {
        public string JsonSerialize<T>(IEnumerable<T> typedList)
        {
            return JsonSerializer.SerializeToString(typedList);
        }

        public string JsonSerialize<T>(T item)
        {
            return JsonSerializer.SerializeToString(item);
        }

        public T JsonDeserialize<T>(string data)
        {
            return JsonSerializer.DeserializeFromString<T>(data);
        }

        public ICollection<T> JsonDeserializeToCollection<T>(string data)
        {
            return JsonSerializer.DeserializeFromString<Collection<T>>(data);
        }

        public string FormatToJsonDate(string funkyDateString)
        {
            return DateTimeSerializer.ToWcfJsonDate(DateTime.Parse(funkyDateString));
        }

        public string FormatToJsonWithProperDateFormatting<T>(string json)
        {
            return JsonSerialize(JsonDeserialize<List<T>>(json));
        }
    }
}