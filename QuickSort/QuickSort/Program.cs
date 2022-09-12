using System;
using System.Collections.Generic;

namespace QuickSort
{
    class Program
    {
        static void QuickSort<T>(List<T> list) where T : IComparable<T>
        {
            QuickSort(list, 0, list.Count - 1);
        }
        static void QuickSort<T>(List<T> list, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                int pivot = HoarePartition(list, low, high);

                QuickSort(list, low, pivot - 1);
                QuickSort(list, pivot + 1, high);
            }
        }

        static int HoarePartition<T>(List<T> list, int low, int high) where T : IComparable<T>
        {
            int pivotI = low;

            int i = low - 1; //left "indicator"
            int j = high + 1;//right "indicator"

            while (true)
            {
                do
                {
                    i++;
                } while (list[i].CompareTo(list[pivotI]) < 0);

                do
                {
                    j--;
                } while (list[j].CompareTo(list[pivotI]) > 0);

                if (i >= j)
                {
                    return j;
                }

                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

        }

        static int LomatuPartition<T>(List<T> list, int low, int high) where T : IComparable<T>
        {
            int pivotIndex = high;

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (list[j].CompareTo(list[pivotIndex]) <= 0)
                {
                    i++;

                    T tempp = list[i];
                    list[i] = list[j];
                    list[j] = tempp;
                }
            }
                T temp = list[i + 1];
                list[i + 1] = list[high];
                list[high] = temp;

            return i + 1;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();

            var nums = new List<int>();
            for(int i = 0; i < 1000; i++)
            {
                nums.Add(rnd.Next(1,100));
            }
            QuickSort(nums);

            ;
        }
    }
}
