using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeZero.Shared.Extensions.Collections
{
    public static class LinkedListExtensions
    {
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
        //     the default comparer.
        public static void Sort<T>(this LinkedList<T> @this)
        {
            @this.Sort(Comparer<T>.Default.Compare);
        }

        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
        //     the specified comparer.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        public static void Sort<T>(this LinkedList<T> @this, IComparer<T> comparer)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;
            @this.Sort(comparer.Compare);
        }

        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
        //     the specified System.Comparison<T>.
        //
        // Parameters:
        //   comparison:
        //     The System.Comparison<T> to use when comparing elements.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     comparison is null.
        public static void Sort<T>(this LinkedList<T> @this, Comparison<T> comparison)
        {
            if (@this == null)
                throw new NullReferenceException();

            if (comparison == null)
                throw new ArgumentNullException("comparison");

            int count = @this.Count;
            if (count <= 1)
                return;

            // merge pairs of lists of doubling size
            for (int mergeLength = 1; mergeLength < count; mergeLength *= 2)
            {
                LinkedListNode<T> mergedTail = null;
                LinkedListNode<T> head2;
                for (LinkedListNode<T> head1 = @this.First; head1 != null; head1 = head2)
                {
                    // skip over the 1st part to the start 2nd
                    head2 = head1;
                    int length1;
                    for (length1 = 0; length1 < mergeLength && head2 != null; ++length1)
                        head2 = head2.Next;

                    // assume we have a full-length 2nd part
                    int length2 = mergeLength;

                    // while we still have items to merge
                    while (length1 > 0 || (length2 > 0 && head2 != null))
                    {
                        LinkedListNode<T> next;

                        // determine which part the next item comes from
                        if (length1 != 0 && !(length2 != 0 && head2 != null && comparison(head1.Value, head2.Value) > 0))
                        {
                            // take item from 1st part
                            Debug.Assert(head1 != null);
                            next = head1;
                            head1 = head1.Next;

                            Debug.Assert(length1 > 0);
                            --length1;
                        }
                        else
                        {

                            // take item from 2nd part
                            Debug.Assert(head2 != null);
                            next = head2;
                            head2 = head2.Next;

                            Debug.Assert(length2 > 0);
                            --length2;
                        }

                        // append the next item to the merged list
                        if (mergedTail == null)
                        {
                            // start a new merged list at the front of the source list
                            if (@this.First != next)    // check for no-op
                            {
                                @this.Remove(next);
                                @this.AddFirst(next);
                            }
                        }
                        else if (mergedTail.Next != next)   // check for no-op
                        {
                            @this.Remove(next);
                            @this.AddAfter(mergedTail, next);
                        }

                        // advance the merged tail
                        mergedTail = next;
                    }
                }
            }
        }
    }
}
