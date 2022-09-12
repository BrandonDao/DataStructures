using System;
using System.Collections.Generic;

namespace UnweightedUndirectedGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new UnweightedUndirectedGraph<char>();

            var a = new Vertex<char>('a');
            var b = new Vertex<char>('b');
            var c = new Vertex<char>('c');

            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);

            graph.AddEdge(a, b);
            graph.AddEdge(b, c);
            graph.AddEdge(c, a);

            var test = graph.BreadthFirstTraversal(a, c);

            ;
        }
    }
}