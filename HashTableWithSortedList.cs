using System;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class HashTableWithSortedList : IOrderSerchStruct<Node>
    {
        private Hashtable internalTable = new Hashtable(10000);        
        private SortedList internalList = new SortedList(new ComparerNode(), 1000);

        #region Miembros de IOrderSerchStruct<Node>

        public int Size
        {
            get { return internalTable.Count; }
        }

        public void Add(Node item)
        {
            internalList.Add(item.FValue, item);
            internalTable.Add(item.GetHashCode(), item);
        }

        public void Clear()
        {
            internalTable.Clear();
            internalList.Clear();
        }

        public bool Contains(Node item)
        {
            return internalTable.ContainsKey(item.GetHashCode());
        }

        public Node FindInStruct(Node item)
        {
            return internalTable[item.GetHashCode()] as Node;
        }

        public bool Remove(Node item)
        {
            internalList.RemoveAt(internalList.IndexOfValue(item));
            internalTable.Remove(item.GetHashCode());
            //TODO DEVOLVER FALSE ALGUNA VEZ!!!
            return true;
        }

        public Node getMinValue()
        {
            return internalList.GetByIndex(0) as Node;
        }

        public Node getMinimoAndRemove()
        {
            Node aux = internalList.GetByIndex(0) as Node;
            this.Remove(aux);
            return aux;
        }

        #endregion
    }
}
