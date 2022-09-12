using System;
using System.Collections.Generic;
using System.Text;

namespace UnweightedUndirectedGraph
{
    public class Vertex<T>
    {
        public T Value;
        public List<Vertex<T>> Neighbors;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<Vertex<T>>();
        }
    }

    public class UnweightedUndirectedGraph<T>
    {
        public List<Vertex<T>> Vertices { get; private set; }

        public UnweightedUndirectedGraph()
        {
            Vertices = new List<Vertex<T>>();
        }

        public bool AddVertex(Vertex<T> vertex)
        {
            // Add if the vertex isn't null, has no neighbors, and isn't already in the graph
            if (vertex != null && vertex.Neighbors.Count == 0 && !Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
                return true;
            }
            return false;
        }
        public bool RemoveVertex(Vertex<T> vertex)
        {
            // Remove if the vertex exists in the graph
            if (Vertices.Contains(vertex))
            {
                foreach (var neighbor in vertex.Neighbors)
                {
                    neighbor.Neighbors.Remove(vertex);
                }
                Vertices.Remove(vertex);

                return true;
            }
            return false;
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b)
        {
            // Add if both vertices aren't null, they both exist in the graph, and aren't connected to each other already
            if (a != null && b != null && Vertices.Contains(a) && Vertices.Contains(b) && !a.Neighbors.Contains(b) && !b.Neighbors.Contains(a))
            {
                a.Neighbors.Add(b);
                b.Neighbors.Add(a);

                return true;
            }
            return false;
        }
        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            // Remove if both vertices aren't null and are neighbors of each other
            if (a != null && b != null && a.Neighbors.Contains(b) && b.Neighbors.Contains(a))
            {
                a.Neighbors.Remove(b);
                b.Neighbors.Remove(a);

                return true;
            }
            return false;
        }

        public Vertex<T> Search(T value)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Value.Equals(value))
                {
                    return vertex;
                }
            }
            return null;
        }

        public List<T> DepthFirstTraversal(Vertex<T> start, Vertex<T> end)
        {
            var vertices = new List<Vertex<T>>();
            DepthFirstTraversal(vertices, start, end);

            var temp = new List<T>();
            foreach(var vertex in vertices)
            {
                temp.Add(vertex.Value);
            }
            return temp;
        }
        private void DepthFirstTraversal(List<Vertex<T>> list, Vertex<T> start, Vertex<T> end)
        {
            if (start == end && list.Count > 0)
            {
                return;
            }

            foreach (var neighbor in start.Neighbors)
            {
                if(list.Contains(neighbor))
                {
                    return;
                }
                list.Add(neighbor);
                DepthFirstTraversal(list, neighbor, end);
            }
        }

        public List<T> BreadthFirstTraversal(Vertex<T> start, Vertex<T> end)
        {
            var vertices = new List<Vertex<T>>();
            var q = new Queue<Vertex<T>>();

            q.Enqueue(start);

            while(q.Count > 0)
            {
                var temp = q.Dequeue();
                vertices.Add(temp);
                if (temp == end)
                {
                    break;
                }

                foreach (var neighbor in temp.Neighbors)
                {
                    if (!vertices.Contains(neighbor))
                    {
                        q.Enqueue(neighbor);
                    }
                }
            }

            var verticesValues = new List<T>();
            foreach(var vertex in vertices)
            {
                verticesValues.Add(vertex.Value);
            }
            return verticesValues;
        }
    }
}
