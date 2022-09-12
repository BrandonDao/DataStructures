using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyCircularlyLinkedList
{
    class Node<T>
    {
        public T Value;
        public Node<T> NextNode;
        public Node<T> PreviousNode;


        public Node(T value)
        {
            Value = value;
        }
    }
}
