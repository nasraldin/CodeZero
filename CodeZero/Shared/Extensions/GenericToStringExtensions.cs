using System.Collections.Generic;
using System.Linq;

namespace CodeZero.Shared.Extensions
{
    /// <summary>
    /// This class is copied from here:
    /// https://github.com/hk1ll3r/CSharp-Collection-Print-Generics
    /// An extensible set of extension methods to pretty print (including contents of) collections in C#.
    /// </summary>
    public static class GenericToStringExtensions
    {
        // Dictionary's KeyValuePair
        public static string ToStringExt<K, V>(this KeyValuePair<K, V> kvp)
        {
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value);
        }

        public static string ToStringExt<K1, K2, V>(this KeyValuePair<K1, Dictionary<K2, V>> kvp)
        {
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value.ToStringExt());
        }

        public static string ToStringExt<K1, V>(this KeyValuePair<K1, List<V>> kvp)
        {
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value.ToStringExt());
        }

        // Dictionary
        public static string ToStringExt<K, V>(this Dictionary<K, V> dic)
        {
            return "{" + string.Join(", ", dic.Select((kvp) => kvp.ToStringExt())) + "}";
        }

        public static string ToStringExt<K, K2, V>(this Dictionary<K, Dictionary<K2, V>> dic)
        {
            return "{" + string.Join(", ", dic.Select((kvp) => kvp.ToStringExt())) + "}";
        }

        public static string ToStringExt<K, V>(this Dictionary<K, List<V>> dic)
        {
            return "{" + string.Join(", ", dic.Select((kvp) => kvp.ToStringExt())) + "}";
        }

        // List
        public static string ToStringExt<T>(this List<T> list) => "[" + string.Join(", ", list) + "]";

        public static string ToStringExt<T>(this List<List<T>> listOfLists) => "[" + string.Join(", ", listOfLists.Select(l => l.ToStringExt())) + "]";

        public static string ToStringExt<K, V>(this List<Dictionary<K, V>> listOfDics) => "[" + string.Join(", ", listOfDics.Select(kvp => kvp.ToStringExt())) + "]";

        public static string ToStringExt<T>(this List<List<List<T>>> list3) => "[" + string.Join(", ", list3.Select(l => l.ToStringExt())) + "]";

        // HashSet
        public static string ToStringExt<T>(this HashSet<T> set) => "[" + string.Join(", ", set) + "]";
    }
}
