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

        public virtual BinaryTreeNode<T> Find(T value) {
            BinaryTreeNode<T> node = this.raiz;
            while (node != null) {
                String values = node.Value + " - " + value;
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


        //TODO ESTA MAL!!!!!
        /**
         * seudo....
         * 
         * def replace_node_in_parent(self, new_value=None):
    '''
    Removes the reference to *self* from *self.parent* and replaces it with *new_value*.
    '''
    if self == self.parent.left_child:
        self.parent.left_child = new_value
    else:
        self.parent.right_child = new_value
    if new_value:
        new_value.parent = self.parent
 
def binary_tree_delete(self, key):
    if key < self.key:
        self.left_child.binary_tree_delete(key)
    elif key > self.key:
        self.right_child.binary_tree_delete(key)
    else: # delete the key here
        if self.left_child and self.right_child: # if both children are present
            # get the smallest node that's bigger than *self*
            successor = self.right_child.findMin()
            self.key = successor.key
            # if *successor* has a child, replace it with that
            # at this point, it can only have a *right_child*
            # if it has no children, *right_child* will be "None"
            successor.replace_node_in_parent(successor.right_child)
        elif self.left_child or self.right_child:   # if the node has only one child
            if self.left_child:
                self.replace_node_in_parent(self.left_child)
            else:
                self.replace_node_in_parent(self.right_child)
        else: # this node has no children
            self.replace_node_in_parent(None)

         * */ 
        //seudo sakado de la wiki
        public virtual bool Remove(BinaryTreeNode<T> removeNode) {
            if (removeNode == null)
                return false;
            if (removeNode.Value.Equals(this.raiz.Value))
            {
                if (removeNode.IsTieneHijos)
                {
                    if (removeNode.HasLeftChild)
                    {
                        this.raiz = removeNode.LeftChild;
                    }
                    else
                        this.raiz = removeNode.RightChild;
                }
                size--;
                return true;
            }

            bool removeLeft = comparer((IComparable)removeNode.Value, (IComparable)removeNode.Parent.Value) <= 0;
            //TODO cuando son iguales???? value== parent=value va a derecha o a izq?
            if (removeLeft)
            {
                removeNode.Parent.LeftChild = null;
                removeNode.Parent = null;
            } else {
                removeNode.Parent.LeftChild = null;
                removeNode.Parent = null;                
            }
            size--;
            return true;
        }

        public void Clear() {
            // TODO
        }

        public virtual void CopyTo(T[] array, int startIndex) {
            // TODO
        }

        public virtual void reOrder()
        {
            //TODO
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

