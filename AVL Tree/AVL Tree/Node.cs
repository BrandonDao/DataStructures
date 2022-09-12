using System;

namespace AVL_Tree
{
    class Node<T>
    {
        public T Value;
        public Node<T> Left;
        public Node<T> Right;

        public int Height;
        public int Balance;
        //public int Height()
        //{
        //    int rightHeight = Right == null ? 0 : Right.Height();
        //    int leftHeight = Left == null ? 0 : Left.Height();
        //    return Math.Max(leftHeight, rightHeight) + 1;
        //}
        //public int Balance()
        //{
        //    int rightHeight = Right == null ? 0 : Right.Height();
        //    int leftHeight = Left == null ? 0 : Left.Height();
        //    return rightHeight - leftHeight;
        //}

        public Node(T value)
        {
            Value = value;
            Height = 1;
            Balance = 0;
        }
    }
}
