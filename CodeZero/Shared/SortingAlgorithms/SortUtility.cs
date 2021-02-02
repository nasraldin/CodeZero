using System;
using System.Collections.Generic;

namespace CodeZero.Shared.SortingAlgorithms
{
    public class SortUtility
    {
        internal static void Swap<T>(ref T item1, ref T item2)
        {
            T dummy = item2;
            item2 = item1;
            item1 = dummy;
        }

        internal static void Heapify<T>(int heapIndex, T[] array, int count, IComparer<T> comparer, SortOrder order)
        {
            // the first descendant 
            int firstdescendantIndex = 2 * heapIndex + 1;
            while (firstdescendantIndex < count)
            {

                switch (order)
                {
                    case SortOrder.Ascending:
                        // check for a second descendant
                        if (firstdescendantIndex + 1 < count)
                            if (comparer.Compare(array[firstdescendantIndex + 1], array[firstdescendantIndex]) > 0) firstdescendantIndex++;

                        // the actual is heap so the job is done
                        if (comparer.Compare(array[heapIndex], array[firstdescendantIndex]) >= 0) return;

                        // otherwise
                        // exchange firstdescendant and heap indexes
                        Swap(ref array[heapIndex], ref array[firstdescendantIndex]);
                        // continue
                        heapIndex = firstdescendantIndex;
                        firstdescendantIndex = 2 * heapIndex + 1;
                        break;
                    case SortOrder.Descending:
                        // check for a second descendant
                        if (firstdescendantIndex + 1 < count)
                            if (comparer.Compare(array[firstdescendantIndex + 1], array[firstdescendantIndex]) < 0) firstdescendantIndex++;

                        // the actual is heap so the job is done
                        if (comparer.Compare(array[heapIndex], array[firstdescendantIndex]) <= 0) return;

                        // otherwise
                        // exchange firstdescendant and heap indexes
                        Swap(ref array[heapIndex], ref array[firstdescendantIndex]);
                        // continue
                        heapIndex = firstdescendantIndex;
                        firstdescendantIndex = 2 * heapIndex + 1;
                        break;
                    default:
                        throw new ApplicationException("The sort order exception should be determined");
                }
            }
        }
    }
}
