using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.util
{
    public static class CollectionUtil
    {
        public static T random<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(Random.Range(0, collection.Count()));
        }

        public static bool isEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.Count() == 0;
        }

        public static void removeIfContained<T>(this ICollection<T> collection, T item)
        {
            if (collection.Contains(item))
                collection.Remove(item);
        }
    }
}