using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace CodeZero.Shared.Extensions
{
    public static class ShufflesExtensions
    {
        /// <summary>
        /// Shuffle an array in O(n) time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] list)
        {
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = list.Length - 1; i > 0; i--)
            {
                int j = r.Next(0, i - 1);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }

        /// <summary>
        /// Shuffle an ArrayList in O(n) time (fastest possible way in theory and practice!).
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ArrayList Shuffle(this ArrayList list)
        {
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = r.Next(0, i - 1);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }

        /// <summary>
        /// Shuffle any (I)List with an extension method based on the Fisher-Yates shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Shuffles an IEnumerable list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        //public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        //{
        //    var r = new Random((int)DateTime.Now.Ticks);
        //    var shuffledList = list.Select(x => new { Number = r.Next(), Item = x }).OrderBy(x => x.Number).Select(x => x.Item);
        //    return shuffledList.ToList();
        //}
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

    }
}