using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Log;

namespace Base.Base
{
    public sealed class CacheLinkedList<T> : ICollection<T>, ICollection
    {
        private readonly LinkedList<T> _linkedLists;
        private readonly Queue<LinkedListNode<T>> _cachedNodes;

        public CacheLinkedList()
        {
            _linkedLists = new LinkedList<T>();
            _cachedNodes = new Queue<LinkedListNode<T>>();
        }

        public int Count => _linkedLists.Count;
        public int CachedNodeCount => _cachedNodes.Count;
        public LinkedListNode<T> First => _linkedLists.First;
        public LinkedListNode<T> Last => _linkedLists.Last;
        public bool IsReadOnly => ((ICollection<T>) _linkedLists).IsReadOnly;
        public object SyncRoot => ((ICollection) _linkedLists).SyncRoot;
        public bool IsSynchronized => ((ICollection) _linkedLists).IsSynchronized;

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            _linkedLists.AddAfter(node, newNode);
            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedLists.AddAfter(node, newNode);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            _linkedLists.AddBefore(node, newNode);
            return newNode;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedLists.AddBefore(node, newNode);
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            _linkedLists.AddFirst(node);
            return node;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            _linkedLists.AddFirst(node);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            _linkedLists.AddLast(node);
            return node;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            _linkedLists.AddLast(node);
        }

        public void Clear()
        {
            LinkedListNode<T> current = _linkedLists.First;
            while (current != null)
            {
                ReleaseNode(current);
                current = current.Next;
            }

            _linkedLists.Clear();
        }

        public void ClearCachedNodes()
        {
            _cachedNodes.Clear();
        }

        public bool Contains(T value)
        {
            return _linkedLists.Contains(value);
        }

        public void CopyTo(T[] array, int index)
        {
            _linkedLists.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection) _linkedLists).CopyTo(array, index);
        }

        public LinkedListNode<T> Find(T value)
        {
            return _linkedLists.Find(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return _linkedLists.FindLast(value);
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = _linkedLists.Find(value);
            if (node != null)
            {
                _linkedLists.Remove(node);
                ReleaseNode(node);
                return true;
            }

            return false;
        }

        public void Remove(LinkedListNode<T> node)
        {
            _linkedLists.Remove(node);
            ReleaseNode(node);
        }

        public void RemoveFirst()
        {
            LinkedListNode<T> first = _linkedLists.First;
            if (first == null)
            {
                D.Error("First is invalid.");
            }

            _linkedLists.RemoveFirst();
            ReleaseNode(first);
        }

        public void RemoveLast()
        {
            LinkedListNode<T> last = _linkedLists.Last;
            if (last == null)
            {
                D.Error("Last is invalid.");
            }

            _linkedLists.RemoveLast();
            ReleaseNode(last);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(_linkedLists);
        }

        private LinkedListNode<T> AcquireNode(T value)
        {
            LinkedListNode<T> node = null;
            if (_cachedNodes.Count > 0)
            {
                node = _cachedNodes.Dequeue();
                node.Value = value;
            }
            else
            {
                node = new LinkedListNode<T>(value);
            }

            return node;
        }

        private void ReleaseNode(LinkedListNode<T> node)
        {
            node.Value = default(T);
            _cachedNodes.Enqueue(node);
        }

        void ICollection<T>.Add(T value)
        {
            AddLast(value);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<T>
        {
            private LinkedList<T>.Enumerator m_Enumerator;

            internal Enumerator(LinkedList<T> linkedList)
            {
                if (linkedList == null)
                {
                    D.Error("Linked list is invalid.");
                }
                else
                {
                    m_Enumerator = linkedList.GetEnumerator();
                }
            }

            public T Current => m_Enumerator.Current;

            object IEnumerator.Current => m_Enumerator.Current;

            public void Dispose()
            {
                m_Enumerator.Dispose();
            }

            public bool MoveNext()
            {
                return m_Enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                ((IEnumerator<T>) m_Enumerator).Reset();
            }
        }
    }
}