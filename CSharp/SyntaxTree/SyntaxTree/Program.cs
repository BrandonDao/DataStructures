using System.Text.RegularExpressions;

namespace SyntaxTree
{
    class Program
    {
        private static readonly List<Regex> regexList = new()
        {
            new Regex(@"^\d+.?\d+"), // floats
            new Regex(@"^[a-zA-Z]"), // variables
            new Regex(@"^[+\-*\/^]") // operators
        };

        static List<string> Tokenize(string input)
        {
            var tokens = new List<string>();
            ReadOnlySpan<char> span = input.Replace(" ", "");

            while (!span.IsEmpty)
            {
                var count = span.Length;

                foreach (var regex in regexList)
                {
                    Match match = regex.Match(span.ToString());
                    if (!match.Success) continue;

                    span = span.Slice(match.Length);
                    tokens.Add(match.Value);
                }
                if (count == span.Length) throw new ArgumentException("Invalid input!");
            }

            return tokens;
        }


        //static Dictionary<char, Func<Operator>> OpMap = new()
        //{
        //    [' '] = () => null,
        //    ['+'] = () => new Operator,
        //};

        //// Shunting Yard Algorithm
        //static string ConvertToRPN(string input)
        //{
        //    Stack<Operator> opStack = new();
        //    string output = "";

        //    foreach (char c in input)
        //    {
        //        if (!OpMap.ContainsKey(c))
        //        {
        //            output += c;
        //            continue;
        //        }

        //        Operator op = OpMap[c].Invoke();

        //        if (op == null) continue;

        //        if (opStack.Count != 0 && op.Precedence <= opStack.Peek().Precedence)
        //        {
        //            output += opStack.Pop().Value;
        //        }
        //        opStack.Push(op);
        //    }
        //    while (opStack.Count != 0)
        //    {
        //        output += opStack.Pop().Value;
        //    }

        //    return output;
        //}


        static void Main()
        {
            //ConvertToRPN("!a + c + !d + b");
            var tokens = Tokenize("0010.312x + b*123.89024379236/34e^z");
        }
    }
}