using System.Collections.Generic;

namespace BubbleSort
{
    public static class BubbleSort<T>
    {
        /// <summary>
        /// Sorts an array of items using the default comparer
        /// </summary>
        /// <param name="items">The array to sort</param>
        public static void Sort(T[] items)
        {
            var comp = Comparer<T>.Default;

            for(int i = 0; i < items.Length; i++)
            {
                for(int x = 0; x < items.Length; x++)
                {
                    if (comp.Compare(items[i], items[x]) > 0) continue;

                    T temp = items[x];
                    items[x] = items[i];
                    items[i] = temp;
                }
            }
        }

        /// <summary>
        /// Sorts an array of items
        /// </summary>
        /// <param name="items">The array to sort</param>
        /// <param name="comparer">The comparer used to determine sort order</param>
        public static void Sort(T[] items, Comparer<T> comparer)
        {
            for (int i = 0; i < items.Length; i++)
            {
                for (int x = 0; x < items.Length; x++)
                {
                    if (comparer.Compare(items[i], items[x]) > 0) continue;
                    
                    T temp = items[x];
                    items[x] = items[i];
                    items[i] = temp;
                }
            }
        }
    }
}
