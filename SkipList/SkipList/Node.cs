using System;

namespace SkipList
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value;
        public int Height;
        public Node<T> Next;
        public Node<T> Bottom;

        public Node(T value, int height)
        {
            Value = value;
            Height = height;
        }
    }
}
