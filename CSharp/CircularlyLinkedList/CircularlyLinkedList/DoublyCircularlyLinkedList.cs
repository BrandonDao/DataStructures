using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyCircularlyLinkedList
{
    class DoublyCircularlyLinkedList<T>
    {
        public Node<T> Head;
        public Node<T> Tail;
        public int Count { get; private set; }
        public void AddFirst(T value)
        {
            if (Head == null)
            {
                Head = new Node<T>(value);
                Tail = Head;
                Head.PreviousNode = Tail;
                Head.NextNode = Tail;
            }
            else
            {
                Node<T> nodeToAdd = new Node<T>(value);
                nodeToAdd.NextNode = Head;
                Head = nodeToAdd;
                Head.PreviousNode = Tail;
            }
            Count++;
        }

        public void AddLast(T value)
        {
            if (Head == null)
            {
                Head = new Node<T>(value);
                Tail = Head;
            }
            else
            {
                Node<T> nodeToAdd = new Node<T>(value);
                Tail.NextNode = nodeToAdd;
                nodeToAdd.PreviousNode = Tail;
                nodeToAdd.NextNode = Head;
                Tail = nodeToAdd;
            }
            Count++;
        }

        public void AddBefore(T Value, T valueToAdd)
        {
            //Do not add before Head
            Node<T> nodeToAdd = new Node<T>(valueToAdd);
            Node<T> currentNode = Head;

            if (Head != null && Count > 1)
            {
                while (!currentNode.NextNode.Value.Equals(Value))
                {
                    currentNode = currentNode.NextNode;
                }

                nodeToAdd.NextNode = currentNode.NextNode;
                nodeToAdd.PreviousNode = currentNode;
                currentNode.NextNode = nodeToAdd;
                Count++;
            }
            else if (Count == 1)
            {
                nodeToAdd.NextNode = Head;
                nodeToAdd.PreviousNode = Head;
                Head.NextNode = nodeToAdd;
                Head.PreviousNode = nodeToAdd;
                Tail = Head;
                Head = nodeToAdd;
                Count++;
            }
            else
            {
                Console.WriteLine("There are no values to add before!");
            }
        }

        public void AddAfter(T Value, T valueToAdd)
        {
            //Do not add after Tail
            Node<T> nodeToAdd = new Node<T>(valueToAdd);
            Node<T> currentNode = Head;
            if (Head != null && Count > 1)
            {
                while (!currentNode.Value.Equals(Value))
                {
                    currentNode = currentNode.NextNode;
                }
                if (currentNode.NextNode == null)
                {
                    currentNode.NextNode = nodeToAdd;
                    nodeToAdd.PreviousNode = currentNode;
                    Tail = nodeToAdd;
                }
                else
                {
                    nodeToAdd.NextNode = currentNode.NextNode;
                    currentNode.NextNode = nodeToAdd;
                    nodeToAdd.PreviousNode = currentNode;
                }
                Count++;
            }
            else if (Count == 1)
            {
                Tail = nodeToAdd;
                Tail.PreviousNode = Head;
                Tail.NextNode = Head;
                Head.NextNode = Tail;
                Head.PreviousNode = Tail;
                Count++;
            }
        }

        public bool RemoveFirst()
        {
            if (Head == null)
            {
                return false;
            }
            else
            {
                Head.NextNode.PreviousNode = Tail;
                Head = Head.NextNode;
                Count--;
                return true;
            }
        }

        public bool RemoveLast()
        {
            Node<T> currentNode = Head;
            if (Head == null)
            {
                return false;
            }
            else 
            {
                while (currentNode.NextNode != Tail)
                {
                    currentNode = currentNode.NextNode;
                }

                currentNode.NextNode = Head;
                Tail = currentNode;
                Count--;
                return true;
            }
        }

        public bool Remove(T Value)
        {//Do not remove Head or Tail
            if (Contains(Value) && Head != null && Count > 1)
            {
                Node<T> nodeToRemove = Find(Value);
                Node<T> currentNode = Head;

                while (currentNode.NextNode != nodeToRemove)
                {
                    currentNode = currentNode.NextNode;
                }

                currentNode.NextNode = nodeToRemove.NextNode;
                nodeToRemove.NextNode.PreviousNode = currentNode;
                Count--;
                return true;
            }
            if (Count < 2)
            {
                Clear();
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Head = null;
        }

        public Node<T> Find(T Value)
        {
            Node<T> nodeToFind = new Node<T>(Value);
            Node<T> currentNode = Head;
            do
            {
                if (nodeToFind.Value.Equals(currentNode.Value))
                {
                    return currentNode;
                }
                currentNode = currentNode.NextNode;
            } while (!currentNode.Value.Equals(Tail.Value));

            return null;
        }

        public bool Contains(T Value)
        {
            Node<T> valueLookedFor = new Node<T>(Value);
            Node<T> currentNode = Head;

            do
            {
                if (valueLookedFor.Value.Equals(currentNode.Value))
                {
                    return true;
                }
                currentNode = currentNode.NextNode;
            } while (!currentNode.Value.Equals(Tail.Value));

            return false;
        }
    }
        
}
