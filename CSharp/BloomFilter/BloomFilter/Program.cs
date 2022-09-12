using System;
using System.Collections.Generic;

namespace BloomFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            var filter = new BloomFilter<int>(100000);
            var itemsAdded = new int[100000];

            var rng = new Random();

            for(int i = 0; i < 100000; i++)
            {
                var num = rng.Next(-10000, 10000000);
                filter.Insert(num);
                itemsAdded[i] = num;
            }

            foreach(var num in itemsAdded)
            {
                if (!filter.ProbablyContains(num)) throw new Exception($"Expected item '{num}' not found!");
            }

            ;
        }
    }
}
