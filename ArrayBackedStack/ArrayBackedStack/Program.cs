namespace ArrayBackedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<int>(1);

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            ;

            var test = stack.Peek() == stack.Pop();
            var testt = stack.Pop();

            ;
        }
    }
}
