using System;
using System.Collections.Generic;

namespace Flies.Shared.Extensions
{
    public static class ListExtensions
    {
        public static int IndexOfFirst<T>(this IList<T> list, Func<T, bool> predicate = null)
        {
            if (predicate == null)
            {
                return list.Count > 0
                    ? 0
                    : -1;
            }

            for (var i = 0; i < list.Count; i++)
                if (predicate(list[i]))
                    return i;
            return -1;
        }

        public static bool RemoveFirst<T>(this IList<T> list, Func<T, bool> predicate = null)
        {
            if (list.Count <= 0)
                return false;

            if (predicate == null)
            {
                list.RemoveAt(0);
                return true;
            }

            var index = list.IndexOfFirst(predicate);
            if (index == -1)
                return false;
            list.RemoveAt(index);
            return true;
        }
    }
}
