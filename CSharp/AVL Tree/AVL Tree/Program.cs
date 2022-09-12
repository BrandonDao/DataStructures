using System;
using System.Collections.Generic;

namespace AVL_Tree
{
    class Program
    {
        static bool IsSorted<T>(List<T> items)
            where T : IComparable<T>
        {
            for(int i = 0; i < items.Count - 1; i++)
            {
                if (items[i].CompareTo(items[i + 1]) == 1)
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            var tree = new AvlTree<int>();
            Random gen = new Random();
            for (int i = 0; i < 100000; i++)
            {
                tree.Add(gen.Next(0, 100000));
            }

            List<int> inOrderData = tree.InOrder();
            bool isSorted = IsSorted(inOrderData);

            List<int> breadthFirstData = tree.BreadthFirst();
            ;
        }
    }
}
