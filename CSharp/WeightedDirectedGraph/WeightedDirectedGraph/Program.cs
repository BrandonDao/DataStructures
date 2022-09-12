namespace WeightedDirectedGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new WeightedDirectedGraph<char>();

            var a = new Vertex<char>('a');
            var b = new Vertex<char>('b');
            var c = new Vertex<char>('c');
            var d = new Vertex<char>('d');
            var e = new Vertex<char>('e');
            var f = new Vertex<char>('f');
            var g = new Vertex<char>('g');
            var h = new Vertex<char>('h');
            var i = new Vertex<char>('i');

            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddVertex(h);
            graph.AddVertex(i);

            graph.AddEdge(a, b, 1);
            graph.AddEdge(a, d, 1);
            graph.AddEdge(b, c, 1);
            graph.AddEdge(b, e, 1);
            graph.AddEdge(d, e, 1);
            graph.AddEdge(d, g, 1);
            graph.AddEdge(c, f, 1);
            graph.AddEdge(g, h, 1);
            graph.AddEdge(e, b, 1);
            graph.AddEdge(e, d, 1);
            graph.AddEdge(e, f, 1);
            graph.AddEdge(e, h, 1);
            graph.AddEdge(f, e, 1);
            graph.AddEdge(h, e, 1);
            graph.AddEdge(h, i, 1);
            graph.AddEdge(f, i, 1);

            var test = graph.DijkstraPathfinderidk(graph, a, i);

            ;
        }
    }
}
