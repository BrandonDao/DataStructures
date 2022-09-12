using System;

namespace HuffmanCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawString = "Hello World!";
            

            var compressedString = rawString.Compress();
            
            var tree = rawString.GetTree();

            var decompressedString = compressedString.Decompress(tree);

            
            ;
        }
    }
}
