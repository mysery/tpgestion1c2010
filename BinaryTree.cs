using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class BinaryTree<T> : ICollection<T> where T : IComparable {

        private BinaryTreeNode<T> raiz;
        private Comparison<IComparable> comparer = CompareElements;
        private int size;

        public virtual BinaryTreeNode<T> Raiz
        {
            get { return raiz; }
            set { raiz = value; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual int Count
        {
            get { return size; }
        }

        public BinaryTree()
        {
            raiz = null;
            size = 0;
        }

        public static int CompareElements(IComparable valor1, IComparable valor2) {
            return valor1.CompareTo(valor2);
        }

        public virtual void Add(T value) {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            this.Add(node);
        }

        /// <summary>
        /// Si el arbol no tiene nodos, coloca el nodo como raiz, sino, si el nodo es de valor
        /// menor a la raiz, lo coloca a la izquierda, sino a la derecha.
        /// </summary>
        /// <param name="node"></param>
        public virtual void Add(BinaryTreeNode<T> node) {
            if (this.raiz == null) {
                this.raiz = node;
                size++;
            } else {
                if (node.Parent == null) {
                    node.Parent = raiz;
                }
                bool insertLeft = comparer((IComparable)node.Value, (IComparable)node.Parent.Value) <= 0;

                if (insertLeft) {
                    if (node.Parent.LeftChild == null) {
                        node.Parent.LeftChild = node;
                        size++;
                    } else {
                        node.Parent = node.Parent.LeftChild;
                        this.Add(node);
                    }
                } else {
                    if (node.Parent.RightChild == null) {
                        node.Parent.RightChild = node;
                        size++;
                    } else {
                        node.Parent = node.Parent.RightChild;
                        this.Add(node);
                    }
                }
            }
        }

        //TODO juli - Vieja este metodo tambien va a sacar el nodo?
        //Te acordas que deciamos que lo obtiene y lo saca del arbol.
        public T getMinimo() {
            if (this.raiz != null) {
                BinaryTreeNode<T> node = this.raiz;
                while (node.HasLeftChild) {
                    node = node.LeftChild;
                }
                return node.Value;
            }
            //return null;
            return default(T); // no tengo idea que hace esto.
        }

        public virtual BinaryTreeNode<T> Find(T value) {
            BinaryTreeNode<T> node = this.raiz;
            while (node != null) {
                if (node.Value.Equals(value)) {
                    return node;
                } else {
                    bool searchLeft = comparer((IComparable)value, (IComparable)node.Value) < 0;
                    if (searchLeft) {
                        node = node.LeftChild;
                    } else {
                        node = node.RightChild;
                    }
                }
            }
            return null;
        }

        public virtual bool Contains(T value) {
            return (this.Find(value) != null);
        }


        public virtual bool Remove(T value) {
            BinaryTreeNode<T> node = Find(value);
            return this.Remove(node);
        }

        public virtual bool Remove(BinaryTreeNode<T> removeNode) {
            // TODO
            return false;
        }

        public void Clear() {
            // TODO
        }

        public virtual void CopyTo(T[] array, int startIndex) {
            // TODO
        }


        public virtual IEnumerator<T> GetEnumerator() {
            return null;
        }

        
        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }


    }
}
