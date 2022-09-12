using System;

namespace Insertion_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            //(Below) Code to create an int array.
            /*
            Console.Write("How many values do you want to sort: ");
            int[] num = new int[int.Parse(Console.ReadLine())];
            for (int i = 0; i < num.Length; i++)
            {
                Console.Write("Value to sort:");
                num[i] = int.Parse(Console.ReadLine());
            }
            */

            //(Below) Code to create a string array.
            Console.Write("How many names do you want to sort: ");
            string[] names = new string[int.Parse(Console.ReadLine())];
            for (int i = 0; i < names.Length; i++)
            {
                Console.Write("Name to sort:");
                names[i] = Console.ReadLine();
            }

            GenericInsertionSortLogic<string>.InsertionSort(names);

            //(Below) Code to write out the sorted array.
            Console.Write("This is your sorted list: ");
            for (int i = 0; i < names.Length; i++)
            {
                Console.Write($"{names[i]}, ");
            }

        }
    }
}
