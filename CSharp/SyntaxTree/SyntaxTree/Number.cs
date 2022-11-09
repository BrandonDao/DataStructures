namespace SyntaxTree
{
    public class Number : INode
    {
        public double Value { get; }

        public Number(double value) { Value = value; }
    }
}