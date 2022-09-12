using System;

namespace Trie
{
    class Program
    {
        static void Main(string[] args)
        {
            var trie = new Trie();

            trie.Insert("a");
            trie.Insert("app");
            trie.Insert("all");
            trie.Insert("apple");

            var test = trie.GetAllMatchingPrefix("a");
            ;
        }
    }
}
