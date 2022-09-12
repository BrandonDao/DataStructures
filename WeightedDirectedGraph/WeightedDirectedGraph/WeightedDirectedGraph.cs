using HeapTree;
using System;
using System.Collections.Generic;

namespace WeightedDirectedGraph
{
    public class Vertex<T> : IComparable<Vertex<T>>
    {
        public T Value;
        public List<Edge<T>> Edges;

        public int x;
        public int y;
        public float Distance;
        public float FinalDistance;
        public Vertex<T> Parent;
        public bool IsVisited;

        public Vertex(T value)
        {
            Value = value;
            Edges = new List<Edge<T>>();
        }

        public int CompareTo(Vertex<T> other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
    public class Edge<T>
    {
        public Vertex<T> StartPoint;
        public Vertex<T> EndPoint;
        public float Weight;

        public Edge(Vertex<T> startPoint, Vertex<T> endPoint, float weight)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Weight = weight;
        }
    }
    public class WeightedDirectedGraph<T>
    {
        private readonly List<Vertex<T>> vertices;

        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public IReadOnlyList<Edge<T>> Edges
        {
            get
            {
                var edges = new List<Edge<T>>();

                foreach (var vertex in vertices)
                {
                    foreach (var edge in vertex.Edges)
                    {
                        edges.Add(edge);
                    }
                }

                return edges;
            }
        }

        public WeightedDirectedGraph()
        {
            vertices = new List<Vertex<T>>();
        }

        public bool AddVertex(Vertex<T> vertex)
        {
            if (vertex != null && vertex.Edges.Count == 0 && !vertices.Contains(vertex))
            {
                vertices.Add(vertex);
                return true;
            }
            return false;
        }
        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (vertices.Contains(vertex))
            {
                foreach (var item in vertices)
                {
                    foreach (var edge in item.Edges)
                    {
                        if (edge.EndPoint.Equals(vertex))
                        {
                            item.Edges.Remove(edge);
                        }
                    }
                }
                vertices.Remove(vertex);
                return true;
            }
            return false;
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b, float distance)
        {
            if (vertices.Contains(a) && vertices.Contains(b) && GetEdge(a, b) == null)
            {
                a.Edges.Add(new Edge<T>(a, b, distance));
                return true;
            }
            return false;
        }
        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            var edge = GetEdge(a, b);
            if (edge != null)
            {
                edge.StartPoint.Edges.Remove(edge);
                return true;
            }
            return false;
        }

        public Vertex<T> Search(T value)
        {
            foreach (var vertex in vertices)
            {
                if (vertex.Value.Equals(value))
                {
                    return vertex;
                }
            }
            return null;
        }
        public Edge<T> GetEdge(Vertex<T> a, Vertex<T> b)
        {
            if (vertices.Contains(a) && vertices.Contains(b))
            {
                foreach (var edge in a.Edges)
                {
                    if (edge.EndPoint == b)
                    {
                        return edge;
                    }
                }
            }
            return null;
        }

        public List<T> DepthFirstTraversal(Vertex<T> start, Vertex<T> end)
        {
            var vertices = new List<Vertex<T>>() { start };

            DepthFirstTraversal(vertices, start, end);

            var temp = new List<T>();
            foreach (var vertex in vertices)
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

            foreach (var edge in start.Edges)
            {
                if (list.Contains(edge.EndPoint))
                {
                    return;
                }
                list.Add(edge.EndPoint);
                DepthFirstTraversal(list, edge.EndPoint, end);
            }
        }

        public List<T> BreadthFirstTraversal(Vertex<T> start, Vertex<T> end)
        {
            var vertices = new List<Vertex<T>>();
            var q = new Queue<Vertex<T>>();

            q.Enqueue(start);

            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                vertices.Add(temp);
                if (temp == end)
                {
                    break;
                }

                foreach (var edge in temp.Edges)
                {
                    if (!vertices.Contains(edge.EndPoint))
                    {
                        q.Enqueue(edge.EndPoint);
                    }
                }
            }

            var verticesValues = new List<T>();
            foreach (var vertex in vertices)
            {
                verticesValues.Add(vertex.Value);
            }
            return verticesValues;
        }

        public List<Vertex<T>> DijkstraPathfinderidk(WeightedDirectedGraph<T> graph, Vertex<T> start, Vertex<T> end)
        {
            var PriorityQ = new MinHeapTree<Vertex<T>>();

            foreach (var vertex in graph.Vertices)
            {
                vertex.Distance = float.MaxValue;
                vertex.Parent = null;
                vertex.IsVisited = false;
            }
            
            start.Distance = 0;
            PriorityQ.Insert(start);

            Vertex<T> current;
            while (!end.IsVisited && PriorityQ.Count > 0)
            {
                current = PriorityQ.Pop();
                current.IsVisited = true;

                foreach(var edge in current.Edges)
                {
                    var tentativeDist = current.Distance + edge.Weight;
                    if (tentativeDist < edge.EndPoint.Distance)
                    {
                        edge.EndPoint.Distance = tentativeDist;
                        edge.EndPoint.Parent = edge.StartPoint;
                        edge.EndPoint.IsVisited = false;
                    }
                    if (!edge.EndPoint.IsVisited && !PriorityQ.Contains(edge.EndPoint))
                    {
                        PriorityQ.Insert(edge.EndPoint);
                    }
                }
            }

            current = end;
            var temp = new List<Vertex<T>>() { current };
            while(current != start)
            {
                temp.Add(current.Parent);
                current = current.Parent;
            }

            // Reverse because it moves from the end vertex through parents to the start
            temp.Reverse();
            return temp;
        }

        //public List<Vertex<T>> AStarPathfinder(WeightedDirectedGraph<T> graph, Vertex<T> start, Vertex<T> end)
        //{
        //    var PriorityQ = new Queue<Vertex<T>>();

        //    foreach (var vertex in graph.Vertices)
        //    {
        //        vertex.Distance = float.MaxValue;
        //        vertex.Parent = null;
        //        vertex.IsVisited = false;
        //    }

        //    start.Distance = 0;
        //}
        //int Manhattan(Vertex<T> start, Vertex<T> end)
        //{
            
        //}
    }
}