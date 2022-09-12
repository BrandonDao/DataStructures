using System;
using System.Collections.Generic;

namespace MergeSort
{
    class Program
    {

        static void MergeSort<T>(List<T> list) where T : IComparable<T>
        {
            if (list.Count > 1)
            {
                List<T> left = new List<T>();
                List<T> right = new List<T>();

                int mid = list.Count / 2;

                for (int i = 0; i < mid; i++)
                {
                    left.Add(list[i]);
                }
                for(int i = mid; i < list.Count; i++)
                {
                    right.Add(list[i]);
                }

                MergeSort(left);
                MergeSort(right);

                Merge(list, left, right);
            }
        }

        static void Merge<T>(List<T> list, List<T> left, List<T> right) where T : IComparable<T>
        {
            int currentLeftI = 0;
            int currentRightI = 0;
            list.Clear();

            while(currentLeftI < left.Count && currentRightI < right.Count)
            {
                if (left[currentLeftI].CompareTo(right[currentRightI]) < 0)
                {
                    list.Add(left[currentLeftI]);
                    currentLeftI++;
                }
                else
                {
                    list.Add(right[currentRightI]);
                    currentRightI++;
                }
            }

            while (currentLeftI < left.Count)
            {
                list.Add(left[currentLeftI]);
                currentLeftI++;
            }
            while(currentRightI < right.Count)
            {
                list.Add(right[currentRightI]);
                currentRightI++;
            }

        }

        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 10, 1, 5, 6, 2, 3, 3, 7, 2, 8, 4, 9 };
            MergeSort(list);
        }
    }
}
