using System;

namespace DoublyCircularlyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyCircularlyLinkedList<int> list = new DoublyCircularlyLinkedList<int>();

            list.AddFirst(0);
            list.AddAfter(0, 1);
            list.AddLast(2);
            Console.WriteLine(list.Tail.NextNode.Value);
            Console.WriteLine(list.Head.PreviousNode.Value);
        }
    }
}
