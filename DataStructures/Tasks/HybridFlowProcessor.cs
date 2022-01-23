using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> _data = new DoublyLinkedList<T>();

        public int Length => _data.Length;
        public bool IsEmpty => _data.Length == 0;

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException();
            }

            T element = _data.ElementAt(0);
            _data.RemoveAt(0);
            return element;
        }

        public void Enqueue(T toBeAdded)
        {
            _data.Add(toBeAdded);
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException();
            }

            T element = _data.ElementAt(_data.Length - 1);
            _data.RemoveAt(_data.Length - 1);
            return element;
        }

        public void Push(T toBeAdded)
        {
            _data.Add(toBeAdded);
        }
    }
}
