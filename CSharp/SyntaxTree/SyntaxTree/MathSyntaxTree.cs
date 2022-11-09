using System.Text.RegularExpressions;

namespace SyntaxTree
{
    public static class Extensions
    {
        private static readonly Dictionary<Regex, Func<string, INode>> regexToINode = new()
        {
            [new Regex(@"^\d+\.?\d*")] = (input) => new Number(double.Parse(input)),
            [new Regex(@"^[a-zA-Z]")] = (input) => new Variable(input),
            [new Regex(@"^[+\-*\/^]")] = (input) => new Operator(input)
        };
        public static List<INode> Tokenize(this string input)
        {
            var tokens = new List<INode>();
            ReadOnlySpan<char> span = input.Replace(" ", "");

            while (!span.IsEmpty)
            {
                var count = span.Length;

                foreach (Regex regex in regexToINode.Keys)
                {
                    Match match = regex.Match(span.ToString());
                    if (!match.Success) continue;

                    INode node = regexToINode[regex].Invoke(match.Value);
                    if (node.GetType() == typeof(Variable))
                    {
                        tokens.Add(new Operator("*"));
                    }
                    tokens.Add(node);

                    span = span.Slice(match.Length);
                    break;
                }
                if (count == span.Length) throw new ArgumentException("Input could not be parsed!");
            }

            return tokens;
        }

        // Djikstra's Shunting Yard Algorithm https://en.wikipedia.org/wiki/Shunting_yard_algorithm
        // (Implementation ignores parentheses)
        public static Stack<INode> ConvertToRPN(this List<INode> tokens)
        {
            var outputQ = new Queue<INode>();
            var opStack = new Stack<Operator>();

            foreach (INode token in tokens)
            {
                if (token.GetType() != typeof(Operator))
                {
                    outputQ.Enqueue(token);
                    continue;
                }

                if (opStack.Count > 0 && ((Operator)token).Priority <= opStack.Peek().Priority)
                {
                    outputQ.Enqueue(opStack.Pop());
                }
                opStack.Push((Operator)token);
            }
            while (opStack.Count > 0)
            {
                outputQ.Enqueue(opStack.Pop());
            }

            var outputStack = new Stack<INode>(outputQ.Count);
            while (outputQ.Count > 0)
            {
                outputStack.Push(outputQ.Dequeue());    // Not ideal
            }
            return outputStack;
        }
    }



    public class MathSyntaxTree
    {
        public INode Root { get; }

        private INode GetChildren(INode curr, Stack<INode> rpnStack)
        {
            if (curr.GetType() != typeof(Operator)) return curr;

            ((Operator)curr).Children[0] = GetChildren(rpnStack.Pop(), rpnStack);
            ((Operator)curr).Children[1] = GetChildren(rpnStack.Pop(), rpnStack);
            return curr;
        }
        public MathSyntaxTree(string expression)
        {
            Stack<INode> rpnStack = expression.Tokenize().ConvertToRPN();

            Root = rpnStack.Pop();

            if (rpnStack.Count == 1) return;

            GetChildren(Root, rpnStack);
        }
    }
}
