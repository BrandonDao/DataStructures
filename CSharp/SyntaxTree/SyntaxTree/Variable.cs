using System.Diagnostics;

namespace SyntaxTree
{
    [DebuggerDisplay("Variable: {Name,nq}")]
    public class Variable : INode
    {
        public string Name { get; }

        public Variable(string name) { Name = name; }
    }
}