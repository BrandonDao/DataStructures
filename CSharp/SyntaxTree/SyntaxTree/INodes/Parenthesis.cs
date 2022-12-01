using System.Diagnostics;

namespace SyntaxTree.INodes
{
    [DebuggerDisplay("{IsLeft?\"(\":\")\",nq}")]
    public class Parenthesis : INode
    {
        public bool IsLeft { get; }

        public Parenthesis(string input)
        {
            IsLeft = input[0] == '(';
        }
    }
}