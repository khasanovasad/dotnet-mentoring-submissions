using System.Collections.Generic;

namespace Tasks.DoNotChange
{
    public interface IDoublyLinkedList<T> : IEnumerable<T>
    {
        public int Length { get; }

        void Add(T toBeAdded);

        void AddAt(int index, T e);

        void Remove(T toBeFound);

        T RemoveAt(int index);

        T ElementAt(int index);
    }
}
