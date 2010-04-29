using System;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class HashTableWithTree : IOrderSerchStruct<Node>
    {
        private Hashtable internalTable = new Hashtable(20000);
        private BinaryTree<Node> internalTree = new BinaryTree<Node>();

        #region Miembros de IOrderSerchStruct<Node>

        public int Size
        {
            get { return internalTable.Count; }
        }

        public void Add(Node item)
        {
            BinaryTreeNode<Node> node = new BinaryTreeNode<Node>(item);
            internalTree.Add(node);
            internalTable.Add(item.GetHashCode(), node);
        }

        public void Clear()
        {
            internalTable.Clear();
            internalTree.Clear();
        }

        public bool Contains(Node item)
        {
            return internalTable.ContainsKey(item.GetHashCode());
        }

        public Node FindInStruct(Node item)
        {
            return (internalTable[item.GetHashCode()] as BinaryTreeNode<Node>).Value;
        }

        public bool Remove(Node item)
        {
            BinaryTreeNode<Node> updateNode = internalTree.Remove(internalTable[item.GetHashCode()] as BinaryTreeNode<Node>);
            internalTable.Remove(item.GetHashCode());
            if (updateNode != null && updateNode.Value != null)
            {
                internalTable.Remove(updateNode.Value.GetHashCode());
                internalTable.Add(updateNode.Value.GetHashCode(), updateNode);
            }
            return true;
        }

        public Node getMinValue()
        {
            return internalTree.getMinValue();
        }

        public Node getMinimoAndRemove()
        {
            Node min = internalTree.getMinValue();
            this.Remove(min);
            return min;
        }

        #endregion
    }
}
