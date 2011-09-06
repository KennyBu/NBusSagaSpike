using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NBTY.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static void ForEach(this IEnumerable items, Action<object> action)
        {
            items.AsQueryable().Cast<object>().ForEach(action);
        }

        public static void VisitAllItemsWith<T>(this IEnumerable<T> items, IVisitor<T> visitor)
        {
            items.ForEach(visitor.Process);
        }

        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            return new List<T> {item};
        }
    }
}