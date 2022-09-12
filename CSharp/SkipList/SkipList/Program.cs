namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new SkipList<int>
            {
                50,
                45,
                5,
                40,
                10,
                35,
                15,
                30,
                20,
                26
            };

            var arr = new int[14] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            list.CopyTo(arr, 2);
            ;
        }
    }
}