using System.Diagnostics;

namespace SyntaxTree.INodes
{
    [DebuggerDisplay("{Value,nq}")]
    public class Operator : INode
    {
        public enum Priorities
        {
            Low,
            Medium,
            High
        }
        private static readonly Dictionary<string, Priorities> priorityMap = new()
        {
            ["+"] = Priorities.Low,
            ["-"] = Priorities.Low,
            ["*"] = Priorities.Medium,
            ["/"] = Priorities.Medium,
            ["^"] = Priorities.High
        };

        public string Value { get; }
        public Priorities Priority { get; }
        public INode[] Children { get; }

        public Operator(string value)
        {
            Value = value;
            Priority = priorityMap[value];
            Children = new INode[2];
        }
    }
}