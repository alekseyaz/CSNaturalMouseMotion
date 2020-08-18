using System.Collections.Generic;

namespace CSNaturalMouseMotion
{
    public static class Exception
    {
        public static void AddAll<T>(this ICollection<T> result, ICollection<T> fromSubTree)
        {
            foreach (T dict in fromSubTree)
            {
                result.Add(dict);
            }
        }

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
