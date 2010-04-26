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
                        //EJECUTO CON LOOP INFINOTO!!!!!
                        //DESPUES VEO PORQUE :P 
                        //Da loop porque no se remueven los nodos que entran al abierto ... falta implementar el remove :P ahi agrege funciones a implementar.
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
            //SI NO TENES IDEA COMO LLEGO ACA???? JAJAJA COPY PASTE????
        }

        /**
         * Obtiene el minimo y ademas lo remueve de la lista.
         */
        public T getMinimoAndRemove()
        {
        if (this.raiz != null) {
            BinaryTreeNode<T> node = this.raiz;
            while (node.HasLeftChild) {
                node = node.LeftChild;
            }
            this.Remove(node);
            return node.Value;
        }
        return default(T); // no tengo idea que hace esto.
        //SI NO TENES IDEA COMO LLEGO ACA???? JAJAJA COPY PASTE????
        }
        
        /// <summary>
        /// Busca un nodo.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual BinaryTreeNode<T> Find(T value) {
/*
            BinaryTreeNode<T> node = this.raiz;
            while (node != null)
            {
                String values = node.Value + " - " + value;
                if (node.Value.Equals(value))
                {
                    return node;
                }
                else
                {
                    bool searchLeft = comparer((IComparable)value, (IComparable)node.Value) < 0;
                    if (searchLeft)
                    {
                        node = node.LeftChild;
                    }
                    else
                    {
                        node = node.RightChild;
                    }
                }
            }
            return null;
*/
            return this.Find(value, this.raiz);
        }

        /// <summary>
        /// Busqueda secuencial sobre todo el arbol!!!!
        /// //TODO PERFORMAR
        /// </summary>
        /// <param name="value"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual BinaryTreeNode<T> Find(T value, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
                BinaryTreeNode<T> nodeLeftChilds = this.Find(value, node.LeftChild);
                if (nodeLeftChilds != null)
                {
                    return nodeLeftChilds;
                }
                BinaryTreeNode<T> nodeRigthChilds = this.Find(value, node.RightChild);
                if (nodeRigthChilds != null)
                {
                    return nodeRigthChilds;
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


        /// <summary>
        /// Remueve un nodo.
        /// </summary>
        /// <param name="removeNode">Nodo a remover.</param>
        /// <returns></returns>
        public virtual bool Remove(BinaryTreeNode<T> removeNode) {
            if (removeNode == null) {
                return false;
            }
            bool esRaiz = (removeNode == raiz);
            if (this.Count == 1)
            {
                this.raiz = null;
                size--;
            }
            //Remueve para el caso que no tenga hijos
            else if (removeNode.IsHoja) 
            {
                if (removeNode.IsLeftChild)
                {
                    removeNode.Parent.LeftChild = null;
                }
                else {
                    removeNode.Parent.RightChild = null;
                }
                removeNode.Parent = null;
                size--;
            }
            //Remueve para el caso que tenga un hijo
            else if (removeNode.CantHijos == 1)
            {
                if (removeNode.HasLeftChild)
                {
                    removeNode.LeftChild.Parent = removeNode.Parent;
                    if (esRaiz)
                    {
                        this.raiz = removeNode.LeftChild;
                    }
                    else
                    {

                        if (removeNode.IsLeftChild)
                        {
                            removeNode.Parent.LeftChild = removeNode.LeftChild;
                        }
                        else
                        {
                            removeNode.Parent.RightChild = removeNode.LeftChild;
                        }
                    }
                }
                else
                {
                    removeNode.RightChild.Parent = removeNode.Parent;
                    if (esRaiz)
                    {
                        this.raiz = removeNode.RightChild;
                    }
                    else
                    {
                        if (removeNode.IsLeftChild)
                        {
                            removeNode.Parent.LeftChild = removeNode.RightChild;
                        }
                        else
                        {
                            removeNode.Parent.RightChild = removeNode.RightChild;
                        }
                    }
                }
                removeNode.Parent = null;
                removeNode.LeftChild = null;
                removeNode.RightChild = null;
                size--;
            }
            //Remueve para el caso que tenga 2 hijos
            else
            {
                BinaryTreeNode<T> successorNode = removeNode.LeftChild;
                while (successorNode.RightChild != null)
                {
                    successorNode = successorNode.RightChild;
                }
                removeNode.Value = successorNode.Value;
                this.Remove(successorNode);
            }
            return true;
        }

        public void Clear() {
            // TODO
        }

        public virtual void CopyTo(T[] array, int startIndex) {
            // TODO
        }

        public virtual void balance()
        {
            //TODO
        }

        public virtual IEnumerator<T> GetEnumerator() {
            return null;
        }

        
        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }


    }
}

