using System;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class HashTableWithMinimum : IOrderSerchStruct<Node>
    {
        private Hashtable internalTable = new Hashtable(5000);
        private Node minimum;

        #region Miembros de IOrderSerchStruct<Node>

        public int Size
        {
            get { return internalTable.Count; }
        }

        public void Add(Node item)
        {
            if (minimum == null || item.CompareTo(minimum) < 0)
                minimum = item;
            internalTable.Add(item.GetHashCode(), item);
        }

        public void Clear()
        {
            internalTable.Clear();
            minimum = null;
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
            internalTable.Remove(item.GetHashCode());
            if (item.Equals(minimum))
                this.serchNewMinimun();
            return true;
        }

        public Node getMinValue()
        {
            return minimum;
        }

        public Node getMinimoAndRemove()
        {
            Node aux = minimum;
            this.Remove(minimum);
            return aux;
        }

        #endregion
        
        public void serchNewMinimun()
        {
            minimum = null;
            foreach (Node node in internalTable.Values)
            {
                if (minimum == null || node.CompareTo(minimum) < 0)
                    minimum = node;
            }
        }        
    }
}
