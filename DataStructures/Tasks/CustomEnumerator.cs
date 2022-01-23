using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class CustomEnumerator<T> : IEnumerator<T>
    {
        private DoublyLinkedList<T> _list;
        private int i = -1;

        public CustomEnumerator(DoublyLinkedList<T> list)
        {
            _list = list;
        }
        
        public bool MoveNext()
        {
            ++i;
            return i < _list.Length;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public T Current => _list.ElementAt(i);

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}