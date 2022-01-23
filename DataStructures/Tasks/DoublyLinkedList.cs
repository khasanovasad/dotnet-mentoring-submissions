using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
    
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Length { get; private set; }
        
        public bool IsEmpty => Length == 0;

        public DoublyLinkedList() { }

        public void Add(T toBeAdded)
        {
            var newNode = new Node<T>(toBeAdded);
            
            if (IsEmpty)
            {
                _head = newNode;
                _tail = newNode;
                ++Length;
                return;
            }

            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
            ++Length;
        }

        public void AddAt(int index, T toBeAdded)
        {
            if (IsEmpty || index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == Length)
            {
                Add(toBeAdded);
                return;
            }

            Node<T> newNode = null;
            
            if (index == 0)
            {
                newNode = new Node<T>(toBeAdded);
                _head = newNode;
                _tail = newNode;
                ++Length;
                return;
            }

            var currentNode = _head;
            var currentIndex = 0;
            while (currentNode != null && currentIndex < index)
            {
                currentNode = currentNode.Next;
                ++currentIndex;
            }

            newNode = new Node<T>(toBeAdded);
            currentNode.Prev.Next = newNode;
            newNode.Prev = currentNode.Prev;

            currentNode.Prev = newNode;
            newNode.Next = currentNode;
            ++Length;
        }

        public T ElementAt(int index)
        {
            if (IsEmpty || index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();
            }
            
            var currentNode = _head;
            var currentIndex = 0;
            while (currentNode != null && currentIndex < index)
            {
                currentNode = currentNode.Next;
                ++currentIndex;
            }

            return currentNode.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        public void Remove(T toBeFound)
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException();
            }

            try
            {
                var currentNode = _head;
                while (!EqualityComparer<T>.Default.Equals(currentNode.Value, toBeFound))
                {
                    currentNode = currentNode.Next;
                }

                if (currentNode.Equals(_head))
                {
                    RemoveHead(currentNode);
                    return;
                }

                if (currentNode.Equals(_tail))
                {
                    RemoveTail(currentNode);
                    return;
                }
                
                currentNode.Prev.Next = currentNode.Next;
                currentNode.Next.Prev = currentNode.Prev;
                currentNode = null;
                --Length;
            }
            catch
            {
                // pass
            }
        }

        private void RemoveTail(Node<T> currentNode)
        {
            _tail = currentNode.Prev;
            _tail.Next = null;
            currentNode = null;
            --Length;
            return;
        }

        private void RemoveHead(Node<T> currentNode)
        {
            _head = currentNode.Next;
            _head.Prev = null;
            currentNode = null;
            --Length;
            return;
        }

        public T RemoveAt(int index)
        {
            if (IsEmpty || index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            T returnValue = default;
            try
            {
                var currentNode = _head;
                int currentIndex = 0;
                while (currentNode != null && currentIndex != index)
                {
                    currentNode = currentNode.Next;
                    ++currentIndex;
                }

                if (currentNode.Equals(_head))
                {
                    returnValue = _head.Value;
                    RemoveHead(currentNode);
                }

                if (currentNode.Equals(_tail))
                {
                    returnValue = _tail.Value;
                    RemoveTail(currentNode);
                }

                currentNode.Prev.Next = currentNode.Next;
                currentNode.Next.Prev = currentNode.Prev;
                returnValue = currentNode.Value;
                currentNode = null;
                --Length;
            }
            catch
            {
                // pass
            }
            
            return returnValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
