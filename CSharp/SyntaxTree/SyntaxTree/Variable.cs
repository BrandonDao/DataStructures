namespace SyntaxTree
{
    public class Variable : INode
    {
        public string Name { get; }

        public Variable(string name) { Name = name; }
    }
}