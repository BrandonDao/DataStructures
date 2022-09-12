using System;

namespace DoublyLinkedListClass
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            list.AddFirst(0);
            list.AddAfter(0, 1);
            list.AddLast(2);

            Console.WriteLine(list.Tail.PreviousNode.Value);

            list.Remove(1);

            ;
        }
    }
}
