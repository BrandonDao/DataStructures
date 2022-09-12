using System;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new int[10] { 10, 1, 9, 2, 8, 3, 7, 4, 6, 5 };
            BubbleSort<int>.Sort(nums);

            ;
        }
    }
}
