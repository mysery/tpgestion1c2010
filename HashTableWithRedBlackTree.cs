using System;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class HashTableWithRedBlackTree : IOrderSerchStruct<Node>
    {
        private Hashtable internalTable = new Hashtable();
        private RedBlackTree<Node> internalRedBlackTree = new RedBlackTree<Node>();

        #region Miembros de IOrderSerchStruct<Node>

        public int Size
        {
            get { return internalTable.Count; }
        }

        public void Add(Node item)
        {
            RedBlackNode<Node> node = new RedBlackNode<Node>(item);
            internalRedBlackTree.Add(node);
            internalTable.Add(item.GetHashCode(), node);
        }

        public void Clear()
        {
            internalTable.Clear();
            internalRedBlackTree.Clear();
        }

        public bool Contains(Node item)
        {
            return internalTable.ContainsKey(item.GetHashCode());
        }

        public Node FindInStruct(Node item)
        {
            return (internalTable[item.GetHashCode()] as RedBlackNode<Node>).Value;
        }

        public bool Remove(Node item)
        {
            if (internalRedBlackTree.Remove(item))
            {
                internalTable.Remove(item.GetHashCode());
                return true;
            }
            return false;
        }

        public Node getMinValue()
        {
            return internalRedBlackTree.getMinValue();
        }

        public Node getMinimoAndRemove()
        {
            Node min = internalRedBlackTree.getMinimoAndRemove();
            internalTable.Remove(min.GetHashCode());
            return min;
        }

        #endregion
    }
}
