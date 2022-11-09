namespace SyntaxTree
{
    public class Operator : INode
    {
        public enum Types
        {
            Unary,
            Binary,
            Ternary
        };

        public string Value { get; }
        public Types Type { get; }
        public INode[] Children { get; }

        public Operator(string value)
        {
            Value = value;
            Children = Array.Empty<INode>();
        }
        public Operator(string value, Types type, INode[] children)
        {
            Value = value;
            Type = type;
            Children = children;
        }
    }
}