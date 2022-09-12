using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipList
{
    public class SkipList<T> : ICollection<T> where T : IComparable<T>
    {
        private Node<T> head;

        private int count;
        public int Count => count;
        public bool IsReadOnly => false;

        public SkipList()
        {
            count = 0;
            head = new Node<T>(default, 1);
        }

        public bool Contains(T val)
        {
            return Search(val) != null;
        }
        private Node<T> Search(T val)
        {
            var current = head;
            while (current != null)
            {
                var comp = current.Next != null ? val.CompareTo(current.Next.Value) : -1;
                if (comp == 0)
                {
                    return current.Next;
                }
                else if (comp < 0)
                {
                    current = current.Bottom;
                }
                else
                {
                    current = current.Next;
                }
            }
            return null;
        }


        private void Connect(Node<T> previous, Node<T> node, Node<T> next)
        {
            node.Next = next;
            if (previous != null)
            {
                previous.Next = node;
            }
        }
        public int GetHeight()
        {
            var rng = new Random();
            var height = 1;
            while (rng.Next(2) == 1)
            {
                height++;
            }
            return height;
        }
        public void Add(T val)
        {
            count++;
            var newNode = new Node<T>(val, GetHeight());

            while (head.Height < newNode.Height)
            {
                var temp = new Node<T>(default, head.Height + 1) { Bottom = head };
                head = temp;
            }

            var current = head;

            while (current != null)
            {
                while (true)
                {
                    var comp = current.Next != null ? val.CompareTo(current.Next.Value) : -1;
                    if (comp < 0 && current.Height == newNode.Height)
                    {
                        break;
                    }
                    else if (comp < 0)
                    {
                        current = current.Bottom;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }

                Connect(current, newNode, current.Next);

                var temp = newNode.Height - 1 > 0 ? new Node<T>(newNode.Value, newNode.Height - 1) : null;
                newNode.Bottom = temp;
                newNode = temp;

                current = current.Bottom;
            }
        }


        public bool Remove(T val)
        {
            if (!Contains(val))
            {
                return false;
            }

            count--;
            var current = head;

            while (current != null)
            {
                var comp = current.Next != null ? val.CompareTo(current.Next.Value) : -1;
                if (comp == 0)
                {
                    Connect(null, current, current.Next.Next);
                }
                else if (comp < 0)
                {
                    current = current.Bottom;
                }
                else
                {
                    current = current.Next;
                }
            }
            return true;
        }
        public void Clear()
        {
            count = 0;
            head = new Node<T>(default, 1);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < count)
            {
                throw new ArgumentOutOfRangeException("Array must have enough indexes to copy to!");
            }

            var current = head;
            while (current.Height > 1)
            {
                current = current.Bottom;
            }
            current = current.Next;

            for (int i = arrayIndex; i < count + arrayIndex; i++)
            {
                array[i] = current.Value;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while (current.Bottom != null)
            {
                current = current.Bottom;
            }

            while (current.Next != null)
            {
                current = current.Next;
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
