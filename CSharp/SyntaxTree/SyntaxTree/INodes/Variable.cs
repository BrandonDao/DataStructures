using System.Diagnostics;

namespace SyntaxTree.INodes
{
    [DebuggerDisplay("{Name,nq}")]
    public class Variable : INode
    {
        public string Name { get; }

        public Variable(string name) { Name = name; }
    }
}