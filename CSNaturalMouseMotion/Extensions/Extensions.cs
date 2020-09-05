using System.Collections.Generic;

namespace Zaac.CSNaturalMouseMotion.Extensions
{
    public static class LinkedListExtensions
    {
        public static IEnumerable<T> GetReverse<T>(this LinkedList<T> list)
        {
            var el = list.Last;
            while (el != null)
            {
                yield return el.Value;
                el = el.Previous;
            }
        }

    }
}
