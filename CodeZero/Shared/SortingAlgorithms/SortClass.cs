using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeZero.Shared.SortingAlgorithms
{
    /// <summary>
    /// This sort class is a static class that holds exrension methods, those last ones extend
    /// the IEnumerable objects so that they use sort algorithms other than the built in one 
    /// within the microsoft staff. The raison is for performance issues some algorithms are 
    /// avantagous over other ones.
    /// </summary>
    public static class SortClass
    {
        #region exchange sorts

        /// <summary>
        /// This is the SortBubble algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortBubble<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);

            int count = array.Count();
            do
            {
                for (int i = 0; i < count - 1; i++)
                {
                    switch (order)
                    {
                        case SortOrder.Descending:
                            if (comparer.Compare(array[i], array[i + 1]) < 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                            }
                            break;
                        case SortOrder.Ascending:
                            if (comparer.Compare(array[i], array[i + 1]) > 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                            }
                            break;
                        default:
                            throw new ApplicationException("Order sould be precised");
                    }
                }
                count--;

            } while (count > 1);

            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// This is the SortCocktail T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortCocktail<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            int count = array.Count();
            bool flag;
            switch (order)
            {
                case SortOrder.Ascending:
                    do
                    {
                        flag = false;

                        for (int i = 0; i < count - 2; i++)
                        {
                            if (comparer.Compare(array[i], array[i + 1]) > 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            break;
                        }
                        flag = false;

                        for (int i = count - 2; i > 0; i--)
                        {
                            if (comparer.Compare(array[i], array[i + 1]) > 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                                flag = true;
                            }
                        }

                    } while (flag == true);
                    break;
                case SortOrder.Descending:
                    do
                    {
                        flag = false;

                        for (int i = 0; i < count - 2; i++)
                        {
                            if (comparer.Compare(array[i], array[i + 1]) < 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            break;
                        }
                        flag = false;

                        for (int i = count - 2; i > 0; i--)
                        {
                            if (comparer.Compare(array[i], array[i + 1]) < 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + 1]);
                                flag = true;
                            }
                        }

                    } while (flag == true);
                    break;
                default:
                    throw new ApplicationException("The sort order exception should be determined");
            }


            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// This is the SortEvenOdd T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortEvenOdd<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            int count = array.Count();
            int Max = (count % 2 == 0) ? 2 * (count / 2) - 1 : 2 * (count - 1) / 2;

            switch (order)
            {
                case SortOrder.Ascending:
                    for (int i = 0; i < count / 2; i++)
                    {
                        for (int j = 0; j < Max; j++)
                        {
                            if (comparer.Compare(array[j], array[j + 1]) > 0)
                            {
                                SortUtility.Swap(ref array[j], ref array[j + 1]);
                            }
                        }
                        for (int j = 1; j < Max; j++)
                        {
                            if (comparer.Compare(array[j], array[j + 1]) > 0)
                            {
                                SortUtility.Swap(ref array[j], ref array[j + 1]);
                            }
                        }
                    }
                    break;
                case SortOrder.Descending:
                    for (int i = 0; i < count / 2; i++)
                    {
                        for (int j = 0; j < Max; j++)
                        {
                            if (comparer.Compare(array[j], array[j + 1]) < 0)
                            {
                                SortUtility.Swap(ref array[j], ref array[j + 1]);
                            }
                        }
                        for (int j = 1; j < Max; j++)
                        {
                            if (comparer.Compare(array[j], array[j + 1]) < 0)
                            {
                                SortUtility.Swap(ref array[j], ref array[j + 1]);
                            }
                        }
                    }
                    break;
                default:
                    throw new ApplicationException("The sort order exception should be determined");
            }

            for (int i = 0; i < count / 2; i++)
            {
                for (int j = 0; j < Max; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) > 0)
                    {
                        SortUtility.Swap(ref array[j], ref array[j + 1]);
                    }
                }
                for (int j = 1; j < Max; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) > 0)
                    {
                        SortUtility.Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }

            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// This is the SortComb T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortComb<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            int count = array.Count();

            int gap = count;
            bool swapped = true;

            switch (order)
            {
                case SortOrder.Ascending:
                    while (gap > 1 || swapped)
                    {
                        if (gap > 1)
                            gap = (int)(gap / 1.247330950103979);

                        int i = 0;
                        swapped = false;
                        while (i + gap < count)
                        {
                            if (comparer.Compare(array[i], array[i + gap]) > 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + gap]);
                                swapped = true;
                            }
                            i++;
                        }
                    }
                    break;
                case SortOrder.Descending:
                    while (gap > 1 || swapped)
                    {
                        if (gap > 1)
                            gap = (int)(gap / 1.247330950103979);

                        int i = 0;
                        swapped = false;
                        while (i + gap < count)
                        {
                            if (comparer.Compare(array[i], array[i + gap]) < 0)
                            {
                                SortUtility.Swap(ref array[i], ref array[i + gap]);
                                swapped = true;
                            }
                            i++;
                        }
                    }
                    break;
                default:
                    throw new ApplicationException("The sort order exception should be determined");
            }

            while (gap > 1 || swapped)
            {
                if (gap > 1)
                    gap = (int)(gap / 1.247330950103979);

                int i = 0;
                swapped = false;
                while (i + gap < count)
                {
                    if (comparer.Compare(array[i], array[i + gap]) > 0)
                    {
                        SortUtility.Swap(ref array[i], ref array[i + gap]);
                        swapped = true;
                    }
                    i++;
                }
            }

            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// This is the SortGenome T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortGenome<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            int count = array.Count();
            int position;

            switch (order)
            {
                case SortOrder.Ascending:
                    position = 0;
                    while (position < count)
                    {
                        if (position == 0 || comparer.Compare(array[position], array[position - 1]) > 0)
                        {
                            position++;
                        }
                        else
                        {
                            SortUtility.Swap(ref array[position], ref array[position - 1]);
                            position -= 1;
                        }
                    }
                    break;
                case SortOrder.Descending:
                    position = 0;
                    while (position < count)
                    {
                        if (position == 0 || comparer.Compare(array[position], array[position - 1]) < 0)
                        {
                            position++;
                        }
                        else
                        {
                            SortUtility.Swap(ref array[position], ref array[position - 1]);
                            position -= 1; // position = position - 1;
                        }
                    }
                    break;
                default:
                    throw new ApplicationException("The sort order exception should be determined");
            }

            foreach (var item in array)
            {
                yield return item;
            }
        }
        #endregion

        #region selection sorts
        /// <summary>
        /// This is the SortSelection T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortSelection<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            _ = array.Count();
            int minIndex;
            T minValue;

            for (int i = 0; i < array.Length - 1; i++)
            {
                minValue = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    // The sort order
                    switch (order)
                    {
                        case SortOrder.Descending:
                            if (comparer.Compare(array[j], minValue) > 0)
                            {
                                minIndex = j;
                                minValue = array[j];
                                array[minIndex] = array[i];
                                array[i] = minValue;

                            }
                            break;
                        case SortOrder.Ascending:
                            if (comparer.Compare(array[j], minValue) <= 0)
                            {
                                minIndex = j;
                                minValue = array[j];
                                array[minIndex] = array[i];
                                array[i] = minValue;

                            }
                            break;
                        default:
                            throw new ApplicationException("The sort order exception should be determined");
                    }
                }
            }

            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// This is the SortHeap T algorithm method
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="items">The IEnumerable going to be sorted</param>
        /// <param name="comparer">The comparer class used to sort collection objects</param>
        /// <param name="order">The order to follow when sorting objects either ascending or descending sens</param>
        /// <returns>The sorted IEnumerable object</returns>
        static public IEnumerable<T> SortHeap<T>(this IEnumerable<T> items, IComparer<T> comparer, SortOrder order)
        {
            T[] array = Enumerable.ToArray(items);
            int count = array.Length;
            for (int index = count / 2 - 1; index >= 0; index--)
                SortUtility.Heapify(index, array, count, comparer, order);

            while (count > 1)
            {
                count--;
                SortUtility.Swap(ref array[0], ref array[count]);
                SortUtility.Heapify(0, array, count, comparer, order);
            }

            foreach (var item in array)
            {
                yield return item;
            }
        }
        #endregion
    }
}
