using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionAlumno
{
    class BinaryTreeNode<T> where T : IComparable {

        public BinaryTreeNode(T value) {
            this.value = value;
        }

        private T value;
        private BinaryTreeNode<T> leftChild;
        private BinaryTreeNode<T> rightChild;
        private BinaryTreeNode<T> parent;

        
        public virtual T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public virtual BinaryTreeNode<T> LeftChild
        {
            get { return leftChild; }
            set { leftChild = value; }
        }

        public virtual BinaryTreeNode<T> RightChild
        {
            get { return rightChild; }
            set { rightChild = value; }
        }

        public virtual BinaryTreeNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual bool IsHoja
        {
            get { return this.CantHijos == 0; }
        }

        public virtual bool IsTieneHijos
        {
            get { return this.CantHijos > 0; }
        }

        public virtual bool IsLeftChild
        {
            get { return this.Parent != null && this.Parent.LeftChild == this; }
        }

        public virtual bool IsRightChild
        {
            get { return this.Parent != null && this.Parent.RightChild == this; }
        }

        public virtual int CantHijos {
            get {
                int count = 0;
                if (this.LeftChild != null) {
                    count++;
                }
                if (this.RightChild != null) {
                    count++;
                }
                return count;
            }
        }

        public virtual bool HasLeftChild {
            get { return (this.LeftChild != null); }
        }

        public virtual bool HasRightChild {
            get { return (this.RightChild != null); }
        }
    }
}
