using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Common
{
    public static class Collections
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection) => 
            collection.OrderBy(item => Random.value);
    }
}