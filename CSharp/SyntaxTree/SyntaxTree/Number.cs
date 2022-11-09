using System.Diagnostics;

namespace SyntaxTree
{
    [DebuggerDisplay("Number: {Value}")]
    public class Number : INode
    {
        public double Value { get; }

        public Number(double value) { Value = value; }
    }
}