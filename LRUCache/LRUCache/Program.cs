using System;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            LRUCache<int, string> cache = new LRUCache<int, string>(2);
            cache.Put(3, "three");
            cache.Put(2, "two");
            cache.Put(1, "one");

            string value;
            var didFindValue = cache.TryGetValue(2, out value);
            ;
        }
    }
}
