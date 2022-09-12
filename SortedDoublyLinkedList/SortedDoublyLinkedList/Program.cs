namespace SortedDoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new SortedDoublyLinkedList<int>();

            list.Insert(1);
            list.Insert(2);
            list.Insert(4);
            list.Insert(5);
            list.Insert(6);

            bool didDelete = list.Delete(1);

            ;
        }
    }
}
