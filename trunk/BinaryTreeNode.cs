using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionAlumno
{
    class BinaryTreeNode<T> where T : IComparable {

        public BinaryTreeNode()
        {
        }

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

        /// <summary>
        /// Actualiza las referencias moviendo el nodo al a la posicion donde se encontraba el nodo pasado como parametro.
        /// </summary>
        /// <param name="removeNode">Nodo con referencias a remover</param>
        internal void replaceNodeReferences(BinaryTreeNode<T> removeNode)
        {
            //Actualizo las referencias de los hijos.
            //y Remuevo las referencias del padre de este nodo
            if (this.IsLeftChild)
            {
                if (this.HasRightChild)
                {
                    this.Parent.LeftChild = this.RightChild;
                    this.RightChild.Parent = this.Parent;
                } else 
                    if (this.HasLeftChild)
                    {
                        this.Parent.RightChild = this.LeftChild;
                        this.LeftChild.Parent = this.Parent;
                    } else
                        this.Parent.LeftChild = null;                        
            } else {
                if (this.HasLeftChild)
                {
                    this.Parent.RightChild = this.LeftChild;
                    this.LeftChild.Parent = this.Parent;
                } else
                    if (this.HasRightChild)
                    {
                        this.Parent.LeftChild = this.RightChild;
                        this.RightChild.Parent = this.Parent;
                    } else 
                        this.Parent.LeftChild = null;
            }

            //actualizo las referencias del padre que quiero remover a este.
            if (removeNode.IsLeftChild)
                removeNode.Parent.LeftChild = this;
            else
                removeNode.Parent.RightChild = this;
            //Actualizo el padre de este nodo con el padre del nodo a remover.
            this.Parent = removeNode.Parent;
              
            //Actualizo los hijos del nodo con los hijos del nodo a remover.
            removeNode.LeftChild.Parent = this;
            this.LeftChild = removeNode.LeftChild;
            removeNode.RightChild.Parent = this;
            this.RightChild = removeNode.RightChild;
        }
    }
}
