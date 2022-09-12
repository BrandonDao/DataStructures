using System;
using System.Collections.Generic;

namespace AVL_Tree
{
    public class AvlTree<T> where T : IComparable<T>
    {
        Node<T> root;
        public int count;

        public AvlTree()
        {
            root = null;
            count = 0;
        }

        /*public void IterativeAdd(T value)
        {
            Node<T> nodeToAdd = new Node<T>(value);
            if (root == null)
            {
                root = nodeToAdd;
                count++;
                return;
            }

            var currentNode = root;
            while(currentNode.Left != null && currentNode.Right != null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                    currentNode = currentNode.Left;
                else
                    currentNode = currentNode.Right;
            }

            nodeToAdd.Parent = currentNode;
            if (value.CompareTo(currentNode.Value) < 0)
                currentNode.Left = nodeToAdd;
            else
                currentNode.Right = nodeToAdd;
            
            count++;
        }*/
        public void Add(T value)
        {
            count++;
            root = Add(value, root);
        }
        private Node<T> Add(T value, Node<T> current)
        {
            if (current == null)
            {
                return new Node<T>(value);
            }

            var comp = value.CompareTo(current.Value);
            if (comp < 0)
            {
                current.Left = Add(value, current.Left);
            }
            else if (comp >= 0)
            {
                current.Right = Add(value, current.Right);
            }

            return Balance(current);
        }

        void UpdateHeight(Node<T> current)
        {
            int rightHeight = current.Right == null ? 0 : current.Right.Height;
            int leftHeight = current.Left == null ? 0 : current.Left.Height;
            current.Height = Math.Max(leftHeight, rightHeight) + 1;
        }
        void UpdateBalance(Node<T> current)
        {
            int rightHeight = current.Right == null ? 0 : current.Right.Height;
            int leftHeight = current.Left == null ? 0 : current.Left.Height;
            current.Balance = rightHeight - leftHeight;
        }
        void UpdateHeightAndBalance(Node<T> node)
        {
            UpdateHeight(node);
            UpdateBalance(node);
        }

        public void Delete(T value)
        {
            count--;
            root = Delete(value, root);
        }
        private Node<T> Delete(T value, Node<T> current)
        {
            var comp = value.CompareTo(current.Value);
            if (comp == 0)
            {
                if (current.Left != null && current.Right != null)
                {
                    var candidate = current.Left.Right != null ? Max(current.Left) : current.Left;
                    var tempVal = candidate.Value;

                    current.Left = Delete(candidate.Value, current.Left);

                    current.Value = tempVal;
                }
                else if (current.Right != null)
                {
                    return current.Right;
                }
                else if (current.Left != null)
                {
                    return current.Left;
                }
                else
                {
                    return null;
                }
            }
            else if (comp < 0)
            {
                current.Left = Delete(value, current.Left);
            }
            else if (comp >= 0)
            {
                current.Right = Delete(value, current.Right);
            }

            UpdateHeightAndBalance(current);
            return Balance(current);
        }

        private Node<T> Balance(Node<T> node)
        {
            UpdateHeightAndBalance(node);

            if (node.Balance < -1)
            {
                if (node.Left.Balance > 0)
                {
                    node.Left = RotateLeft(node.Left);
                    UpdateHeightAndBalance(node.Left.Left);
                }

                node = RotateRight(node);
                UpdateHeightAndBalance(node.Right);
            }
            else if (node.Balance > 1)
            {
                if (node.Right.Balance < 0)
                {
                    node.Right = RotateRight(node.Right);
                    UpdateHeightAndBalance(node.Right.Right);
                }

                node = RotateLeft(node);
                UpdateHeightAndBalance(node.Left);
            }

            UpdateHeightAndBalance(node);
            return node;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;
            return pivot;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            var pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;
            return pivot;
        }
        private Node<T> Min(Node<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }
            return Min(node.Left);
        }
        private Node<T> Max(Node<T> node)
        {
            if (node.Right == null)
            {
                return node;
            }
            return Max(node.Right);
        }
        
        public List<T> InOrder()
        {
            var vals = new List<T>();
            InOrder(root, vals);
            return vals;
        }

        private void InOrder(Node<T> current, List<T> vals)
        {
            if (current == null)
            {
                return;
            }

            InOrder(current.Left, vals);
            vals.Add(current.Value);
            InOrder(current.Right, vals);
        }

        public List<T> PreOrder()
        {
            var vals = new List<T>();
            PreOrder(root, vals);
            return vals;
        }
        private void PreOrder(Node<T> current, List<T> vals)
        {
            if (current == null)
            {
                return;
            }

            vals.Add(current.Value);
            InOrder(current.Left, vals);
            InOrder(current.Right, vals);
        }

        public List<T> PostOrder()
        {
            var vals = new List<T>();
            PostOrder(root, vals);
            return vals;
        }
        private void PostOrder(Node<T> current, List<T> vals)
        {
            if (current == null)
            {
                return;
            }

            InOrder(current.Left, vals);
            InOrder(current.Right, vals);
            vals.Add(current.Value);
        }

        public List<T> BreadthFirst()
        {
            var vals = new List<T>();
            BreadthFirst(root, vals);
            return vals;
        }
        private void BreadthFirst(Node<T> current, List<T> vals)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(current);

            while (queue.Count != 0)
            {
                var temp = queue.Dequeue();
                vals.Add(temp.Value);
                if (temp.Left != null)
                {
                    queue.Enqueue(temp.Left);
                }
                if (temp.Right != null)
                {
                    queue.Enqueue(temp.Right);
                }
            } 
        }
    }
}
