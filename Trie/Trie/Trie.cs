using System.Collections.Generic;

namespace Trie
{
    public class Node
    {
        public char Val;
        public bool IsWord;
        public Dictionary<char, Node> Children;

        public Node(char val)
        {
            Val = val;
            IsWord = false;
            Children = new Dictionary<char, Node>();
        }
    }

    public class Trie
    {
        public Node Root;

        public Trie()
        {
            Clear();
        }

        public void Insert(string word)
        {
            var current = Root;

            for (int i = 0; i < word.Length; i++)
            {
                if (current.Children.ContainsKey(word[i]))
                {
                    current = current.Children[word[i]];
                }
                else
                {
                    current.Children.Add(word[i], new Node(word[i]));
                    current = current.Children[word[i]];
                }

                if (i == word.Length - 1)
                {
                    current.IsWord = true;
                }
            }
        }
        public bool Delete(string word)
        {
            var node = Search(word);
            if (node != null)
            {
                // Word is part of another
                if (node.Children.Count > 0)
                {
                    node.IsWord = false;
                    return true;
                }
                // Word is not part of another
                else
                {
                    var current = Root;
                    Node lastWord = node;

                    for (int i = 0; i < word.Length; i++)
                    {
                        current = current.Children[word[i]];

                        // Keep track of the last full word on the way to the word to delete
                        // so we can clear its children
                        if (current.IsWord && i != word.Length - 1)
                        {
                            lastWord = current;
                        }
                    }
                    lastWord.Children.Clear();
                    return true;
                }
            }
            return false;
        }
        public void Clear()
        {
            Root = new Node(default);
        }

        public Node Search(string word)
        {
            var current = Root;

            for (int i = 0; i < word.Length; i++)
            {
                if (!current.Children.ContainsKey(word[i]))
                {
                    return null;
                }

                current = current.Children[word[i]];

                if (current.Val == word[^1] && current.IsWord)
                {
                    return current;
                }
            }
            return null;
        }
        public bool Contains(string word)
        {
            return Search(word) != null;
        }

        public List<string> GetAllMatchingPrefix(string prefix)
        {
            var words = new List<string>();
            var node = Search(prefix);

            if(node == null)
            {
                return null;
            }

            GetAllMatchingPrefix(node, words, prefix.Substring(0, prefix.Length - 1));

            return words;
        }
        private void GetAllMatchingPrefix(Node node, List<string> words, string prefix)
        {
            if(node == null)
            {
                return;
            }

            foreach (var kvp in node.Children)
            {
                GetAllMatchingPrefix(kvp.Value, words, prefix + node.Val);
            }

            if(node.IsWord)
            {
                words.Add(prefix + node.Val);
            }
        }
    }
}
