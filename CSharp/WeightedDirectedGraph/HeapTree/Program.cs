using System;
using System.Collections.Generic;

namespace HeapTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new MinHeapTree<int>();
            var list = new List<int>();

            var rng = new Random(0);
            for(int i = 0; i < 1000; i++)
            {
                tree.Insert(rng.Next(0, 10000));
            }

            for (int i = 0; i < 1000; i++)
            {
                list.Add(tree.Pop());
            }

            ;
        }
    }
}
