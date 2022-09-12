using Microsoft.VisualBasic.FileIO;
using System;
using System.Numerics;

namespace RedBlackTree
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value;
        public Node<T> Left;
        public Node<T> Right;
        public bool IsBlack;

        public Node(T value)
        {
            Value = value;
            IsBlack = false;
        }
    }
    public class LLRedBlackTree<T> where T : IComparable<T>
    {
        public Node<T> root;
        public int Count;

        public LLRedBlackTree()
        {
            Count = 0;
        }

        public void Add(T value)
        {
            root = Add(root, value);
            root.IsBlack = true;
        }

        private Node<T> Add(Node<T> node, T value)
        {
            if (node == null)
            {
                Count++;
                return new Node<T>(value);
            }                             // If tree empty
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }    // Splitting 4-nodes

            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = Add(node.Left, value);
            }          // Go left
            else if (value.CompareTo(node.Value) > 0)
            {
                node.Right = Add(node.Right, value);
            }     // Go right
            else
            {
                throw new Exception("A node with the same value already exists");
            }                                          // Duplicate value

            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }                        // RightLeaning --> Left Leaning
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }// Prevent consecutive red nodes

            return node;
        }

        public bool Delete(T value)
        {
            int OriginalCount = Count;
            if (root != null)
            {
                root = Delete(root, value);
                if(root != null)
                {
                    root.IsBlack = true;
                }
            }
            return OriginalCount != Count;
        }

        private Node<T> Delete(Node<T> node, T value)
        {
            if(value.CompareTo(node.Value) < 0)
            {
                if(node.Left != null)
                {
                    if(!IsRed(node.Left) && !IsRed(node.Left.Left))
                    {
                        MoveRedLeft(node);
                    }           // Move Red Down
                    node.Left = Delete(node.Left, value);                        // Go Left
                }
            }
            else
            {
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }                                         // Flip 3-nodes and unbalance 4-nodes
                if(value.CompareTo(node.Value) == 0 && node.Right == null)
                {
                    Count--;
                    return null;
                }    // Val To Del is leaf node
                if(node.Right != null)
                {
                    if(!IsRed(node.Right) && !IsRed(node.Right.Left))
                    {
                        node = MoveRedRight(node);
                    }         // Move red right
                    if(value.CompareTo(node.Value) == 0)
                    {
                        Node<T> temp = Minimum(node.Right);
                        node.Value = temp.Value;
                        node.Right = Delete(node.Right, temp.Value);
                    }                      // Remove node w/ two children
                    else
                    {
                        node.Right = Delete(node.Right, value);                  // Move right
                    }
                }
            }
            return TreeCleanup(node);
        }

        //public bool DeleteMin()
        //{
        //    int OriginalCount = Count;
        //    if (root != null)
        //    {
        //        root = DeleteMin(root);
        //        if (root != null)
        //        {
        //            root.IsBlack = true;
        //        }
        //    }

        //    return OriginalCount != Count;
        //}
        //private Node<T> DeleteMin(Node<T> node)
        //{
        //    if(node.Left == null)
        //    {
        //        Count--;
        //        return null;
        //    }                               // Delete
        //    if(!IsRed(node.Left) && !IsRed(node.Left.Left))
        //    {
        //        node = MoveRedLeft(node);
        //    }     // Carry red node down
        //    node.Left = DeleteMin(node.Left);

        //    return TreeCleanup(node);
        //}

        public bool DeleteMax()
        {
            int OriginalCount = Count;
            if (root != null)
            {
                root = DeleteMax(root);
                if (root != null)
                {
                    root.IsBlack = true;
                }
            }

            return OriginalCount != Count;
        }
        private Node<T> DeleteMax(Node<T> node)
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }

            if(node.Right == null)
            {
                Count--;
                return null;
            }                            // Deleting
            if(!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = MoveRedRight(node);
            } // Carry red node down
            node.Right = DeleteMax(node.Right);
            
            return TreeCleanup(node);
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }


        private Node<T> RotateRight(Node<T> node)
        {
            var pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;

            pivot.IsBlack = node.IsBlack;
            node.IsBlack = false;
            return pivot;
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            var pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;

            pivot.IsBlack = node.IsBlack;
            node.IsBlack = false;
            return pivot;
        }

        private Node<T> MoveRedRight(Node<T> node)
        {
            FlipColor(node);
            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateRight(node);

                FlipColor(node);
                if (IsRed(node.Right.Right))
                {
                    node.Right = RotateLeft(node.Left);
                }
            }

            return node;
        }
        private Node<T> MoveRedLeft(Node<T> node)
        {
            FlipColor(node);
            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColor(node);
            }

            return node;
        }

        private Node<T> TreeCleanup(Node<T> node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }                                                      // Left Leaning Rule
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }                              // Balance a node with 2 left nodes
            if(IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }                                   // Break up 4-node
            if((node.Left != null) && IsRed(node.Left.Right) && !IsRed(node.Left.Left))
            {
                node.Left = RotateLeft(node.Left);

                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }   // Avoid red touching red
            } // Re-enforce Left Leaning Rule

            return node;
        }

        private Node<T> Minimum(Node<T> node)
        {
            if(node.Left == null)
            {
                return node;
            }
            return Minimum(node.Left);
        }
        private Node<T> Maximum(Node<T> node)
        {
            if(node.Right == null)
            {
                return node;
            }
            return Maximum(node.Right);
        }

        private void FlipColor(Node<T> node)
        {
            node.IsBlack = IsRed(node);
            node.Left.IsBlack = IsRed(node.Left);
            node.Right.IsBlack = IsRed(node.Right);
        }
        private bool IsRed(Node<T> node)
        {
            if(node == null)
            {
                return false;
            }
            if (node.IsBlack)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LLRedBlackTree<int> Tree = new LLRedBlackTree<int>();
            Tree.Add(1);
            Tree.Add(2);
            Tree.Add(3);
            Tree.Add(4);
            Tree.Add(5);
            Tree.Add(6);
            Tree.Add(7);
            Tree.Add(8);
            Tree.Add(9);
            Tree.Add(10);

            //Tree.Delete(4);
            //Tree.Delete(5);
            //Tree.Delete(10);
            //Tree.Delete(1);
        }
    }
}