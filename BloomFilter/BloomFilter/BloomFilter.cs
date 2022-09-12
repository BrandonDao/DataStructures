using System;
using System.Collections.Generic;
using System.Text;

namespace BloomFilter
{
    public class BloomFilter<T>
    {
        private bool[] set { get; set; }
        private List<Func<T, int>> hashFunctions { get; set; }

        public int Count { get; private set; }
        public int Capacity => set.Length;

        public BloomFilter(int capacity)
        {
            set = new bool[capacity];
            hashFunctions = new List<Func<T, int>>()
            {
                (T item) => { return item.GetHashCode(); },
                (T item) => { return (item, item).GetHashCode(); },
                (T item) => { return (item.GetHashCode(), item).GetHashCode(); }
            };
        }

        public void LoadHashFunc(Func<T, int> hashFunc)
        {
            hashFunctions.Add(hashFunc);
        }

        public void Insert(T item)
        {
            if (Count == Capacity) throw new ArgumentException("Maximum capacity has already been reached!");

            foreach(var func in hashFunctions)
            {
                set[Math.Abs(func(item)) % Capacity] = true;
            }
            Count++;
        }

        public bool ProbablyContains(T item)
        {
            foreach(var func in hashFunctions)
            {
                if (set[Math.Abs(func(item)) % Capacity] == true) return true;
            }
            return false;
        }
    }
}
