using System.Diagnostics;

namespace SyntaxTree.INodes
{
    [DebuggerDisplay("{Value}")]
    public class Number : INode
    {
        public double Value { get; }

        public Number(double value) { Value = value; }
    }
}