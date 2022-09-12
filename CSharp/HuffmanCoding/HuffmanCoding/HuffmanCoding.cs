using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HuffmanCoding
{
    public class HuffmanNode
    {
        public char Character { get; private set; }
        public int Frequency { get; private set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }
        public bool IsLeaf => Left == null;

        public HuffmanNode(char character, int frequency)
        {
            Character = character;
            Frequency = frequency;
        }
    }
    public class HuffmanNodeComparer : Comparer<HuffmanNode>
    {
        public override int Compare([AllowNull] HuffmanNode x, [AllowNull] HuffmanNode y)
        {
            if (x.Frequency > y.Frequency) return 1;
            else if (x.Frequency < y.Frequency) return -1;

            return 0;
        }
    }

    public static class HuffmanCoding
    {
        public static HuffmanNode GetTree(this string input)
        {
            var frequencyMap = new Dictionary<char, int>();
            foreach (var character in input)
            {
                if (!frequencyMap.ContainsKey(character))
                {
                    frequencyMap.Add(character, 0);
                }

                frequencyMap[character]++;
            }

            var priorityQ = new MinHeapTree<HuffmanNode>(new HuffmanNodeComparer());
            foreach (KeyValuePair<char, int> kvp in frequencyMap)
            {
                priorityQ.Insert(new HuffmanNode(character: kvp.Key, frequency: kvp.Value));
            }

            while (priorityQ.Count > 1)
            {
                HuffmanNode a = priorityQ.Pop();
                HuffmanNode b = priorityQ.Pop();

                priorityQ.Insert(
                    new HuffmanNode(character: default, frequency: a.Frequency + b.Frequency)
                    {
                        Left = a,
                        Right = b
                    });
            }

            return priorityQ.Pop();
        }

        public static string Compress(this string input)
        {
            HuffmanNode treeRoot = input.GetTree();
            Dictionary<char, string> charValMap = GetBinaryMap(treeRoot);

            var compressedString = new StringBuilder();
            foreach (var character in input)
            {
                compressedString.Append(charValMap[character]);
            }
            return compressedString.ToString();
        }

        public static string Decompress(this string input, HuffmanNode treeRoot)
        {
            var output = new StringBuilder();

            if(treeRoot.IsLeaf)
            {
                output.Append(treeRoot.Character, input.Length);
            }
            else
            {
                HuffmanNode curr = treeRoot;
                foreach (var bit in input)
                {
                    curr = bit == '0' ? curr.Left : curr.Right;

                    if (curr.IsLeaf)
                    {
                        output.Append(curr.Character);
                        curr = treeRoot;
                    }
                }
            }

            return output.ToString();
        }

        private static Dictionary<char, string> GetBinaryMap(HuffmanNode treeRoot)
        {
            var map = new Dictionary<char, string>();
            if (treeRoot.IsLeaf)
            {
                map.Add(treeRoot.Character, "0");
                return map;
            }

            GetBinaryMap(treeRoot, map, "");

            return map;
        }
        private static void GetBinaryMap(HuffmanNode current, Dictionary<char, string> map, string path)
        {
            if (current == null) return;

            if (current.IsLeaf)
            {
                map[current.Character] = path;
            }

            GetBinaryMap(current.Left, map, path + "0");
            GetBinaryMap(current.Right, map, path + "1");
        }
    }
}
