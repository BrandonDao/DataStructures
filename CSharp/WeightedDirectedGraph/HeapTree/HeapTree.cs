using System;

namespace HeapTree
{
    public class MinHeapTree<T> where T : IComparable<T>
    {
        public T[] Tree;
        public int Count;

        public MinHeapTree()
        {
            Tree = new T[1];
            Count = 0;
        }

        public void Insert(T value)
        {
            if (Count >= Tree.Length)
            {
                var temp = new T[Tree.Length * 2];
                Tree.CopyTo(temp, 0);
                Tree = temp;
            }

            Count++;
            Tree[Count - 1] = value;

            HeapifyUp(Count - 1);
        }
        public T Pop()
        {
            var root = Tree[0];
            Tree[0] = Tree[Count - 1];
            Tree[Count - 1] = default;

            Count--;
            HeapifyDown(0);
            
            return root;
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            if (index == 0 || Tree[index].CompareTo(Tree[parentIndex]) >= 0)
            {
                return;
            }

            var temp = Tree[parentIndex];
            Tree[parentIndex] = Tree[index];
            Tree[index] = temp;

            HeapifyUp(parentIndex);
        }
        private void HeapifyDown(int index)
        {
            if (index < Count - 1)
            {
                int leftIndex = (index * 2) + 1;
                int leftComp = leftIndex > Count - 1 ? 1 : Tree[leftIndex].CompareTo(Tree[index]);

                int rightIndex = (index + 1) * 2;
                int rightComp = rightIndex > Count - 1 ? 1 : Tree[rightIndex].CompareTo(Tree[index]);

                int childrenComp = leftComp == 1 && rightComp == 1 ? 0 : Tree[leftIndex].CompareTo(Tree[rightIndex]);

                if (leftComp < 0 && childrenComp <= 0)
                {
                    var temp = Tree[leftIndex];
                    Tree[leftIndex] = Tree[index];
                    Tree[index] = temp;
                    HeapifyDown(leftIndex);
                }
                else if (rightComp < 0 && childrenComp > 0)
                {
                    var temp = Tree[rightIndex];
                    Tree[rightIndex] = Tree[index];
                    Tree[index] = temp;
                    HeapifyDown(rightIndex);
                }
            }

            return;
        }

        public bool Contains(T val)
        {
            for(int i = 0; i < Count; i++)
            {
                if(Tree[i].Equals(val))
                {
                    return true;
                }
            }
            return false;
        }

        // parent = (index - 1) / 2
        // left = (index * 2) + 1;
        // right = (index + 1) * 2
    }
}
