using System;

namespace SortedDoublyLinkedList
{
    public class SortedDoublyLinkedList<T> where T : IComparable<T>
    {
        private readonly Node<T> head;

        public SortedDoublyLinkedList()
        {
            head = new Node<T>(default);
        }

        private void Connect(Node<T> previous, Node<T> node, Node<T> next)
        {
            node.Next = next;
            node.Previous = previous;
            if(previous != null)
            {
                previous.Next = node;
            }

            if (next != null)
            {
                next.Previous = node;
            }
        }
        
        public void Insert(T value)
        {
            var current = head;
            while (current.Next != null && value.CompareTo(current.Next.Value) >= 0)
            {
                current = current.Next;
            }

            Connect(current, new Node<T>(value), current.Next);
        }

        public bool Delete(T value)
        {
            var current = head;
            while (current.Next != null && value.CompareTo(current.Value) > 0)
            {
                current = current.Next;
            }

            if (value.CompareTo(current.Value) == 0)
            {
                Connect(current.Previous.Previous, current.Previous, current.Next);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
