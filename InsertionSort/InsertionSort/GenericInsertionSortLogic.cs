using System;
using System.Collections.Generic;
using System.Text;

namespace Insertion_Sort
{
    class GenericInsertionSortLogic<T> where T:IComparable<T>
    {
        static public void InsertionSort(T[] num)
        {
            
            for (int i = 0; i < num.Length; i++)
            {
                int j = i;
                while (j > 0 && num[j].CompareTo(num[j - 1]) < 0)
                {
                    T temp = num[j];
                    num[j] = num[j - 1];
                    num[j - 1] = temp;
                    j--;
                }
            }
 
        }
    }
}
