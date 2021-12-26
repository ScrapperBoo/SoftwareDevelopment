using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CustomDictionary
{
    public class CustomDic<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, ICollection<KeyValuePair<TKey, TValue>>
    {
        const int MaxArrayLimit = 5;

        private int count = 0;

        private LinkedList<KeyValuePair<TKey, TValue>>[] DATA;

        public int Count => count;

        public bool IsReadOnly => false;

        public CustomDic()
        {
            DATA = new LinkedList<KeyValuePair<TKey, TValue>>[8];
        }


        private int GetHashCode(TKey key)
        {
            var hash = key.GetHashCode();
            return ((hash % DATA.Length) + DATA.Length) % DATA.Length;


        }

        public void Add(KeyValuePair<TKey, TValue> elem)
        {
            var index = GetHashCode(elem.Key);

            if (DATA[index] is null)
            {
                DATA[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                DATA[index].AddLast(new KeyValuePair<TKey, TValue>(elem.Key, elem.Value));
                count++;

                if (DATA.Length / 3 <= count)
                {
                    ReCreateArray();
                }
            }
            else
            {
                foreach (var k in DATA[index])
                {
                    if (k.Equals(elem))
                    {
                        throw new ArgumentException("Key already exists.", nameof(elem.Key));
                    }
                }

                DATA[index].AddLast(new KeyValuePair<TKey, TValue>(elem.Key, elem.Value));

            }
        }

        private void ReCreateArray()
        {
            var newLength = DATA.Length * 3;
            count = 0;
            var oldData = DATA;

            DATA = new LinkedList<KeyValuePair<TKey, TValue>>[newLength];

            foreach (var list in oldData)
            {
                if(!(list is null))
                foreach (var k in list)
                {
                    Add(k);
                }
            }
        }


        public bool Search(KeyValuePair<TKey, TValue> elem)
        {
            var k = GetHashCode(elem.Key);
            return DATA[k].Contains(elem);
        }



        public bool Remove(KeyValuePair<TKey, TValue> elem)
        {
            var k = GetHashCode(elem.Key);
            if (DATA[k] == null)
            {
                return false;
            }
            else
            {
                return DATA[k].Remove(elem);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in DATA)
            {
                if(!(linkedList is null))
                foreach (var kvp in linkedList)
                {
                    yield return kvp;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var linkedList in DATA)
            {
                foreach (var kvp in linkedList)
                {
                    yield return kvp;
                }
            }
        }


        public void Clear()
        {
            foreach (var kvp in DATA)
            {
                kvp.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            bool found = false;
            foreach (var hcode in DATA)
            {
                foreach (var elem in hcode)
                {
                    if (item.Equals(elem))
                        found = true;
                }

            }
            return found;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentException("The array cannot be null");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The starting array cannot be negative");

            }
            if (count > array.Length - arrayIndex + 1)
            {
                throw new ArithmeticException("The destination array has fewer elements than the collection.");
            }

            foreach (var lnklst in DATA)
            {
                foreach (var elem in lnklst)
                {
                    var index = GetHashCode(elem.Key);
                    array[index] = new KeyValuePair<TKey, TValue>(elem.Key, elem.Value);

                }
            }

        }

        public class DictEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private LinkedList<KeyValuePair<TKey, TValue>>[] _items;
            private int _index = -1;
            private LinkedListNode<KeyValuePair<TKey, TValue>> _currentNode;

            public DictEnumerator(LinkedList<KeyValuePair<TKey, TValue>>[] _items)
            {
                this._items = _items;

            }

            public KeyValuePair<TKey, TValue> Current => _currentNode.Value;

            object IEnumerator.Current => _currentNode.Value;

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_currentNode != null && _currentNode.Next != null)
                {
                    _currentNode = _currentNode.Next;
                    return true;
                }

                while (_currentNode is null)
                {
                    _index++;

                    if (_index >= _items.Length)
                    {
                        return false;
                    }

                    _currentNode = _items[_index].First;
                }

                return false;
            }

            public void Reset()
            {
                _index = -1;
                _currentNode = null;
            }
        }
    }
}

