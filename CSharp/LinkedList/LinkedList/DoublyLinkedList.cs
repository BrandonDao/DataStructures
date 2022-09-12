using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListClass
{
    class DoublyLinkedList<T>
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
            }
            else
            {
                Node<T> nodeToAdd = new Node<T>(value);
                nodeToAdd.NextNode = Head;
                Head = nodeToAdd;
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
                Tail = nodeToAdd;
            }
            Count++;
        }

        public void AddBefore(T Value, T valueToAdd)
        {
            Node<T> nodeToAdd = new Node<T>(valueToAdd);
            Node<T> currentNode = Head;

            if (Head != null)
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
            else
            {
                Console.WriteLine("There are no values to add before!");
            }
        }

        public void AddAfter(T Value, T valueToAdd)
        {
            Node<T> nodeToAdd = new Node<T>(valueToAdd);
            Node<T> currentNode = Head;
            if (Head != null)
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
        }

        public bool RemoveFirst()
        {
            if (Head == null)
            {
                return false;
            }
            else
            {
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

                currentNode.NextNode = null;
                Tail = currentNode;
                Count--;
                return true;
            }
        }

        public bool Remove(T Value)
        {// && if count < 1 then just clear
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
