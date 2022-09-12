using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HashMap
{
    class DuplicateKeyException : Exception
    {
        public DuplicateKeyException()
            : base("Duplicate keys not supported")
        {
        }
    }

    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                KeyValuePair<TKey, TValue>? kvp = GetKeyValuePair(key);
                if (!kvp.HasValue) throw new KeyNotFoundException();

                return kvp.Value.Value;
            }
            set
            {
                KeyValuePair<TKey, TValue>? kvp = GetKeyValuePair(key);
                
                if (kvp.HasValue)
                {
                    Remove(kvp.Value);
                }
                Add(key, value);
            }
        }

        public IEqualityComparer<TKey> Comparer { get; private set; }

        public ICollection<TKey> Keys { get; private set; }
        public ICollection<TValue> Values { get; private set; }


        private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;

        private int count { get; set; }
        public int Count => count;
        public int Capacity => buckets.Length;
        public bool IsReadOnly => false;

        public HashMap(IEqualityComparer<TKey> comparer, int capacity = 1)
        {
            if (capacity < 1) throw new ArgumentOutOfRangeException("Capacity must be 1 or greater");
            Keys = new List<TKey>();
            Values = new List<TValue>();
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            Comparer = comparer;
        }
        public HashMap(int capacity = 1)
        {
            if (capacity < 1) throw new ArgumentOutOfRangeException("Capacity must be 1 or greater");
            Keys = new List<TKey>();
            Values = new List<TValue>();
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            Comparer = EqualityComparer<TKey>.Default;
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key)) throw new DuplicateKeyException();

            if (Count == Capacity)
            {
                ReHash(Capacity * 2);
            }

            int index = Math.Abs(key.GetHashCode() % Capacity);

            if (buckets[index] == null)
            {
                buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            buckets[index].AddFirst(new KeyValuePair<TKey, TValue>(key, value));
            Keys.Add(key);
            Values.Add(value);
            count++;
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        private void ReHash(int newCapacity)
        {
            var tempBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[newCapacity];

            foreach (var list in buckets)
            {
                if (list == null) continue;

                foreach (var keyValPair in list)
                {
                    int index = Math.Abs(keyValPair.Key.GetHashCode() % newCapacity);

                    if (tempBuckets[index] == null)
                    {
                        tempBuckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    }

                    tempBuckets[index].AddFirst(keyValPair);
                }
            }

            buckets = tempBuckets;
        }

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key)) return false;

            int index = Math.Abs(key.GetHashCode() % Capacity);
            foreach (var keyValPair in buckets[index])
            {
                if (Comparer.Equals(key, keyValPair.Key))
                {
                    buckets[index].Remove(keyValPair);
                    Keys.Remove(keyValPair.Key);
                    Values.Remove(keyValPair.Value);

                    count--;
                    return true;
                }
            }
            return false;
        }
        public bool Remove(KeyValuePair<TKey, TValue> kvp)
        {
            return Remove(kvp.Key);
        }
        public void Clear()
        {
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[1];
            count = 0;

            Keys.Clear();
            Values.Clear();
        }

        private KeyValuePair<TKey, TValue>? GetKeyValuePair(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % Capacity);
            if (buckets[index] == null) return null;

            foreach (var keyValPair in buckets[index])
            {
                if (Comparer.Equals(key, keyValPair.Key))
                {
                    return keyValPair;
                }
            }
            return null;
        }
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var keyValPair = GetKeyValuePair(item.Key);
            if (!keyValPair.HasValue)
            {
                return false;
            }
            if (keyValPair.Value.Value.Equals(item.Value))
            {
                return true;
            }

            return false;

        }
        public bool ContainsKey(TKey key)
        {
            return GetKeyValuePair(key).HasValue;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            value = default;
            var keyValPair = GetKeyValuePair(key);

            if (!keyValPair.HasValue) return false;

            value = keyValPair.Value.Value;
            return true;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (array.Length < count + arrayIndex) throw new ArgumentException("Array's capacity was too small");

            foreach (var list in buckets)
            {
                if (list == null) continue;

                foreach (var keyValPair in list)
                {
                    array[arrayIndex] = keyValPair;
                    arrayIndex++;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in buckets)
            {
                if (list == null) continue;

                foreach (var keyValPair in list)
                {
                    yield return keyValPair;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
