using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
