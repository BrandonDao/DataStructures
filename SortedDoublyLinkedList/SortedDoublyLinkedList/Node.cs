using System;

namespace SortedDoublyLinkedList
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value;
        public Node<T> Previous;
        public Node<T> Next;

        public Node(T value)
        {
            Value = value;
        }
    }
}
