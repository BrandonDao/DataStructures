using System;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    public class LRUCache<TKey, TValue> : ICache<TKey, TValue>
    {
        public int Capacity { get; private set; }
        public int Count => list.Count;
        private LinkedList<KeyValuePair<TKey, TValue>> list;
        private Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> dictionary;

        public LRUCache(int capacity)
        {
            Capacity = capacity;
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
            dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
        }

        public void Put(TKey key, TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> node;

            if (dictionary.ContainsKey(key))
            {
                node = dictionary[key];
                list.Remove(node.Value);
            }
            else
            {
                if (Count + 1 > Capacity)
                {
                    dictionary.Remove(list.Last.Value.Key);
                    list.RemoveLast();
                }

                node = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
                dictionary.Add(key, node);
            }

            node.Value.Value = value;

            list.AddFirst(node);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (!dictionary.ContainsKey(key)) return false;

            LinkedListNode<KeyValuePair<TKey, TValue>> node = dictionary[key];

            value = node.Value.Value;
            list.Remove(node);
            list.AddFirst(node);

            return true;
        }
    }
}
