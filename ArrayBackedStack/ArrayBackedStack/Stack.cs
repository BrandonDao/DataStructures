using System;

namespace ArrayBackedStack
{
    public class Stack<T>
    {
        private T[] items { get; set; }
        public int Count { get; private set; }
        public int Capacity => items.Length;

        public Stack(int initialCapacity = 10)
        {
            if (initialCapacity < 1) throw new ArgumentException("Initial capacity must be 1 or greater!");

            items = new T[initialCapacity];
            Count = 0;
        }

        public void Push(T item)
        {
            if (Count == Capacity)
            {
                Resize();
            }

            items[Count] = item;
            Count++;
        }
        private void Resize()
        {
            T[] temp = new T[Capacity * 2];

            for (int i = 0; i < Capacity; i++)
            {
                temp[i] = items[i];
            }
            items = temp;
        }

        public T Pop()
        {
            if (items[0] == null)
            {
                throw new InvalidOperationException("No items in stack!");
            }

            return items[Count -= 1];
        }

        public T Peek()
        {
            if (items[0] == null)
            {
                throw new InvalidOperationException("No items in stack!");
            }

            return items[Count - 1];
        }
    }
}
