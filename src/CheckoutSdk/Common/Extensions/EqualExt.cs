using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Common.Extensions
{
    internal static class EqualExt
    {
        public static bool EqualsNull(this string source, string other)
        {
            return source == other || source?.Equals(other) == true; 
        }

        public static bool EqualsNull(this int? source, int? other)
        {
            return source == other || source?.Equals(other) == true;
        }

        public static bool EqualsNull<T>(this IList<T> source, IList<T> other)
        {
            return
                source == other
                || source?.Equals(other) == true
                || (source != null && other != null
                    && source.All(x => other.Contains(x)));
        }
    }
}
