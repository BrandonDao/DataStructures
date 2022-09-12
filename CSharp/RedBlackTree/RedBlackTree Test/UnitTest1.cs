using System;
using Xunit;
using System.Collections.Generic;
using RedBlackTree;
using System.Linq;

namespace RedBlackTree_Test
{
    public class UnitTest1
    {
        public int[] GenTreeVals(int count, int min, int max)
        {

            var nums = new HashSet<int>();
            var RNG = new Random();

            while(nums.Count < count)
            {
                nums.Add(RNG.Next(min, max));
            }   // Generate Unique Nums
            int[] TempArr = nums.ToArray();  // Convert HashSet to Array
            return TempArr;
        }
        public LLRedBlackTree<int> GenTree (int[] vals)
        {
            var Tree = new LLRedBlackTree<int>();
            for(int i = 0; i < vals.Length; i++)
            {
                Tree.Add(vals[i]);
            }// Add to Tree

            return Tree;
        }
        public List<int> ListToCompareCountWith(int[] vals)
        {
            var list = new List<int>();
            for (int i = 0; i < vals.Length; i++)
            {
                list.Add(vals[i]);
            }// Add to Tree

            return list;
        }

        [Theory]
        [InlineData(new object[] { 10, 0, 100 })]
        [InlineData(new object[] { 50, 0, 500 })]
        [InlineData(new object[] { 100, 0, 1000 })]
        public void MaintainInvariants(int count, int min, int max)
        {
            var vals = GenTreeVals(count, min, max);
            var Tree = GenTree(vals);

            if (!Tree.root.IsBlack)
            {
                Assert.True(0 == 1, "Root was not black");
            }
            else if (PreOrderCheck(Tree))
            {
                Assert.True(1 == 1);
            }
        }
        bool PreOrderCheck(LLRedBlackTree<int> Tree)
        {
            var stack = new Stack<Node<int>>();
            stack.Push(Tree.root);

            while(stack.Count != 0)
            {
                var temp = stack.Pop();

                if (temp.Right != null)
                {
                    if (!temp.IsBlack && !temp.Right.IsBlack)
                    {
                        Assert.True(0 == 1, "Consecutive Red Nodes");
                    }
                    if (temp.Value > temp.Right.Value)
                    {
                        Assert.True(0 == 1, "Parent was greater than right child");
                    }
                    stack.Push(temp.Right);
                }

                if (temp.Left != null)
                {
                    if (!temp.IsBlack && !temp.Left.IsBlack)
                    {
                        Assert.True(0 == 1, "Consecutive Red Nodes");
                    }
                    if (temp.Value < temp.Left.Value)
                    {
                        Assert.True(0 == 1, "Parent was less than left child");
                    }
                }
            }

            return true;
        }



        [Theory]
        [InlineData(new object[] { 10, 0, 100 })]
        [InlineData(new object[] { 50, 0, 500 })]
        [InlineData(new object[] { 100, 0, 1000 })]
        public void Insert(int count, int min, int max)
        {
            var vals = GenTreeVals(count, min, max);
            var Tree = GenTree(vals);
            var list = ListToCompareCountWith(vals);

            if(Tree.Count != list.Count)
            {
                Assert.True(0 == 1, "Counts do not match");
            }
            
        }
        [Theory]
        [InlineData(new object[] { 10, 0, 100 })]
        [InlineData(new object[] { 50, 0, 500 })]
        [InlineData(new object[] { 100, 0, 1000 })]
        public void Delete(int count, int min, int max)
        {
            var vals = GenTreeVals(count, min, max);
            var Tree = GenTree(vals);
            var list = ListToCompareCountWith(vals);
            var rng = new Random();
            var valToDel = vals[rng.Next(count)];

            Tree.Delete(valToDel);
            list.Remove(valToDel);

            if (Tree.Count != list.Count)
            {
                Assert.True(0 == 1, "Counts do not match");
            }

        }
    }
}
