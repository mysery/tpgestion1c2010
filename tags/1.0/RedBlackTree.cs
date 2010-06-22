///<summary>
///A red-black tree must satisfy these properties:
///
///1. The root is black. 
///2. All leaves are black. 
///3. Red nodes can only have black children. 
///4. All paths from a node to its leaves contain the same number of black nodes.
///</summary>

using System.Collections;
using System.Text;
using System;
using System.Reflection;

namespace SolucionAlumno
{
    /// <summary>
    /// Esta implementacion fue adaptada para nuestro TP, 
    /// Utilizando ademas Generics cosa que pueda ser utilizada por futuros cursantes.
    /// </summary>
    /// <typeparam name="T">Tipo contenido dentro de los nodos.</typeparam>
    public class RedBlackTree<T> : IOrderSerchStruct<T> where T : IComparable
    {
        // cantidad nodos.
        private int size;
        // raiz del arbol.
        private RedBlackNode<T> root;
        //Ultimo nodo buscado, utilizado para optimizar las busquedas.
        private RedBlackNode<T> lastSearchNode;
        //Comparador.
        private Comparison<IComparable> comparer = CompareElements;

        public RedBlackTree()
        {
            root = null;
            lastSearchNode = null;
        }

        /// <summary>
        /// Funcion de comparacion, llama al compareTo del valor
        /// </summary>
        /// <param name="valor1">valor1 a comparar</param>
        /// <param name="valor2">valor2 contra se compara el valor1</param>
        /// <returns>0 igual, 1 mayor, -1 menor </returns>
        public static int CompareElements(IComparable valor1, IComparable valor2)
        {
            return valor1.CompareTo(valor2);
        }

        public void Add(RedBlackNode<T> node)
        {
            RedBlackNode<T> exist = this.Find(node.Value);
            if (exist != null)
            {
                exist.addOverflowItem(node);
            }
            else
            {
                node.Parent = lastSearchNode;
                // insert node into tree starting at parent's location
                if (node.Parent != null)
                {
                    if (comparer(node.Value, (node.Parent.Value)) > 0)
                        node.Parent.Right = node;
                    else
                        node.Parent.Left = node;
                }
                else
                {// first node added
                    root = node;
                }
                RestoreAfterInsert(node); // restore red-black properities
            }

            lastSearchNode = node;
            size++;
        }
        
        /// <summary>
        /// Busca un nodo segun el valor, va asignando la variable lastSearchNode por los nodos recorridos
        /// </summary>
        /// <param name="value">valor que se quiere buscar asociado al nodo.</param>        
        /// <returns></returns>
        public RedBlackNode<T> Find(T value)
        {
            int result;

            //TODO PROBAR ESTO: Busqueda performante ???
           if (lastSearchNode != null && comparer((IComparable)value, (IComparable)lastSearchNode.Value) == 0)
           {
               if (lastSearchNode.IsOverflow && !lastSearchNode.Value.Equals(value))
                   lastSearchNode = lastSearchNode.OverflowParent;
               if (lastSearchNode.hasOverflow() && !lastSearchNode.Value.Equals(value))
               {
                   RedBlackNode<T> auxFind = lastSearchNode.getOverflowItem(value);
                   if (auxFind != null)
                       lastSearchNode = auxFind;
               }
               
               return lastSearchNode;
            }
            //Busqueda desde la raiz
            RedBlackNode<T> treeNode = root;
            while (treeNode != null)
            {
                result = comparer((IComparable)value, (IComparable)treeNode.Value);
                if (result == 0)
                {
                    //Si tiene overflow y no es el nodo buscado, hay que ver cual corresponde de los repetidos.
                    if (treeNode.hasOverflow() && !treeNode.Value.Equals(value))
                    {
                        RedBlackNode<T> auxFind = treeNode.getOverflowItem(value);
                        if (auxFind != null)
                            treeNode = auxFind;
                    }
                    lastSearchNode = treeNode;
                    return treeNode;
                }
                if (result < 0)
                {
                    lastSearchNode = treeNode;
                    treeNode = treeNode.Left;
                }
                else
                {
                    lastSearchNode = treeNode;
                    treeNode = treeNode.Right;
                }
            }
            return null;
        }
                
        ///<summary>
        /// GetMinNode
        /// Devuelve el nodo minimo.
        ///<summary>
        public RedBlackNode<T> GetMinNode()
        {
            RedBlackNode<T> treeNode = root;

            if (treeNode == null)
                throw (new Exception("RedBlack tree is empty"));

            // traverse to the extreme left to find the smallest key
            while (treeNode.Left != null)
                treeNode = treeNode.Left;
            
            //Esto es por performance, ya que remover un item de overflow es mejor que remover el nodo del arbol.
            if (treeNode.hasOverflow())
                treeNode = treeNode.getOverflowItem();
            
            lastSearchNode = treeNode;

            return treeNode;

        }
        ///<summary>
        /// GetMaxNode
        /// Devuelve el nodo maximo.
        ///<summary>
        public RedBlackNode<T> GetMaxNode()
        {
            RedBlackNode<T> treeNode = root;

            if (treeNode == null)
                throw (new Exception("RedBlack tree is empty"));

            // traverse to the extreme right to find the largest key
            while (treeNode.Right != null)
                treeNode = treeNode.Right;

            lastSearchNode = treeNode;

            return treeNode;

        }

        /// <summary>
        /// retorna valor minimo.
        /// </summary>
        /// <returns>El valor contenido en el nodo minimo.</returns>
        public T GetMinValue()
        {
            return GetMinNode().Value;
        }

        /// <summary>
        /// retorna valor maximo.
        /// </summary>
        /// <returns>El valor contenido en el nodo maximo.</returns>
        public object GetMaxValue()
        {
            return GetMaxNode().Value;
        }

        ///<summary>
        /// IsEmpty
        /// Is the tree empty?
        ///<summary>
        public bool IsEmpty()
        {
            return (root == null);
        }

        ///<summary>
        /// Delete
        /// Delete a node from the tree and restore red black properties
        ///<summary>
        public bool Remove(RedBlackNode<T> z)
        {
            if (z == null)
                return false;
            //En el caso de que tenga elementos repetidos solo hay que eliminar el elemento necesario.
            if (z.hasOverflow() || z.IsOverflow)
            {
                if (!z.IsOverflow)
                    z.removeOverflowItem(z.Value);
                else
                {
                    z.OverflowParent.removeOverflowItem(z.Value);
                }
                //El remove con overflow se volvio muy performante para el arbol. yea!!!
            } else {
                // A node to be deleted will be: 
                //		1. a leaf with no children
                //		2. have one child
                //		3. have two children
                // If the deleted node is red, the red black properties still hold.
                // If the deleted node is black, the tree needs rebalancing

                // If strictly internal, first swap position with successor.
                if ((z.Left != null) && (z.Right != null))
                {
                    RedBlackNode<T> s = z.getSuccessor();
                    swapPosition(s, z);
                }
                
                // Start fixup at replacement node, if it exists.
                RedBlackNode<T> auxReplaceNode = ((z.Left != null) ? z.Left : z.Right);
                if (auxReplaceNode != null)
                {
                    // Link replacement to parent
                    auxReplaceNode.Parent = z.Parent;
                    if (z.Parent == null)
                        root = auxReplaceNode;
                    else
                        if (z == z.Parent.Left)
                            z.Parent.Left = auxReplaceNode;
                        else
                            z.Parent.Right = auxReplaceNode;

                    // Null out links so they are OK to use by fixAfterDeletion.
                    z.Left = z.Right = z.Parent = null;

                    // Fix replacement
                    if (z.Color == RedBlackNode<T>.Colors.BLACK)
                        RestoreAfterDelete(auxReplaceNode);
                }
                else
                {
                    if (z.Parent == null)
                    {
                        root = null;
                    }
                    else
                    {
                        if (z.Color == RedBlackNode<T>.Colors.BLACK)
                            RestoreAfterDelete(z);

                        if (z.Parent != null)
                        {
                            if (z == z.Parent.Left)
                                z.Parent.Left = null;
                            else
                                if (z == z.Parent.Right)
                                    z.Parent.Right = null;
                            
                            z.Parent = null;
                        }
                    }
                }
            }

            lastSearchNode = null;
            size--;
            return true;
        }

        ///<summary>
        /// Clear
        /// Empties or clears the tree
        ///<summary>
        public void Clear()
        {
            root = null;
            size = 0;
        }

        #region IOrderSerchStruct<T> Members

        public int Size
        {
            get { return this.size; }
        }

        public bool Contains(T value)
        {
            return (this.Find(value) != null);
        }

        public T FindInStruct(T value)
        {
            return this.Find(value).Value;
        }

        public bool Remove(T value)
        {
            if (value == null)
                throw (new Exception("RedBlackNode remove value is null"));

            // find node
            RedBlackNode<T> node = this.Find(value);
            if (node == null)
                return false;// valor no encontrado.

            return this.Remove(node);
        }

        /// <summary>
        /// Agregar nodo.
        /// </summary>
        /// <param name="data">Objeto que contiene la informacion del nodo.</param>
        public void Add(T data)
        {
            if (data == null)
                throw new Exception("RedBlackNode and data must not be null");

            RedBlackNode<T> node = new RedBlackNode<T>(data);
            this.Add(node);
        }

        public T getMinValue()
        {
            return this.GetMinNode().Value;
        }

        public T getMinimoAndRemove()
        {
            RedBlackNode<T> node = this.GetMinNode();
            T value = node.Value;
            this.Remove(node);
            return value;
        }

        #endregion

        #region AuxMethods for restores
        private RedBlackNode<T>.Colors colorOf(RedBlackNode<T> n)
        {
            return (n == null ? RedBlackNode<T>.Colors.BLACK : n.Color);
        }

        private RedBlackNode<T> parentOf(RedBlackNode<T> n)
        {
            return (n == null ? null : n.Parent);
        }

        private void setColor(RedBlackNode<T> n, RedBlackNode<T>.Colors c)
        {
            if (n != null)
                n.Color = c;
        }

        private RedBlackNode<T> leftOf(RedBlackNode<T> n)
        {
            return (n == null) ? null : n.Left;
        }

        private RedBlackNode<T> rightOf(RedBlackNode<T> n)
        {
            return (n == null) ? null : n.Right;
        }
        #endregion

        #region Restore Methods
        ///<summary>
        /// RestoreAfterInsert
        /// Additions to red-black trees usually destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it
        ///</summary>
        private void RestoreAfterInsert(RedBlackNode<T> n)
        {
            n.Color = RedBlackNode<T>.Colors.RED;

            while ((n != null) &&
               (n != root) &&
               (n.Parent.Color == RedBlackNode<T>.Colors.RED))
            {
                if (parentOf(n) == leftOf(parentOf(parentOf(n))))
                {
                    RedBlackNode<T> y = rightOf(parentOf(parentOf(n)));
                    if (colorOf(y) == RedBlackNode<T>.Colors.RED)
                    {
                        setColor(parentOf(n), RedBlackNode<T>.Colors.BLACK);
                        setColor(y, RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(parentOf(n)), RedBlackNode<T>.Colors.RED);
                        n = parentOf(parentOf(n));
                    }
                    else
                    {
                        if (n == rightOf(parentOf(n)))
                        {
                            n = parentOf(n);
                            RotateLeft(n);
                        }

                        setColor(parentOf(n), RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(parentOf(n)), RedBlackNode<T>.Colors.RED);
                        if (parentOf(parentOf(n)) != null)
                            RotateRight(parentOf(parentOf(n)));
                    }
                }
                else
                {
                    RedBlackNode<T> y = leftOf(parentOf(parentOf(n)));
                    if (colorOf(y) == RedBlackNode<T>.Colors.RED)
                    {
                        setColor(parentOf(n), RedBlackNode<T>.Colors.BLACK);
                        setColor(y, RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(parentOf(n)), RedBlackNode<T>.Colors.RED);
                        n = parentOf(parentOf(n));
                    }
                    else
                    {
                        if (n == leftOf(parentOf(n)))
                        {
                            n = parentOf(n);
                            RotateRight(n);
                        }
                        setColor(parentOf(n), RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(parentOf(n)), RedBlackNode<T>.Colors.RED);
                        if (parentOf(parentOf(n)) != null)
                            RotateLeft(parentOf(parentOf(n)));
                    }
                }
            }
            root.Color = RedBlackNode<T>.Colors.BLACK;
        }

        ///<summary>
        /// RestoreAfterDelete
        /// Deletions from red-black trees may destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it
        ///</summary>
        private void RestoreAfterDelete(RedBlackNode<T> x)
        {
            while ((x != root) && (colorOf(x) == RedBlackNode<T>.Colors.BLACK))
            {
                if (x == leftOf(parentOf(x)))
                {
                    RedBlackNode<T> sib = rightOf(parentOf(x));

                    if (colorOf(sib) == RedBlackNode<T>.Colors.RED)
                    {
                        setColor(sib, RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(x), RedBlackNode<T>.Colors.RED);
                        RotateLeft(parentOf(x));
                        sib = rightOf(parentOf(x));
                    }

                    if ((colorOf(leftOf(sib)) == RedBlackNode<T>.Colors.BLACK) &&
                        (colorOf(rightOf(sib)) == RedBlackNode<T>.Colors.BLACK))
                    {
                        setColor(sib, RedBlackNode<T>.Colors.RED);
                        x = parentOf(x);
                    }
                    else
                    {
                        if (colorOf(rightOf(sib)) == RedBlackNode<T>.Colors.BLACK)
                        {
                            setColor(leftOf(sib), RedBlackNode<T>.Colors.BLACK);
                            setColor(sib, RedBlackNode<T>.Colors.RED);
                            RotateRight(sib);
                            sib = rightOf(parentOf(x));
                        }
                        setColor(sib, colorOf(parentOf(x)));
                        setColor(parentOf(x), RedBlackNode<T>.Colors.BLACK);
                        setColor(rightOf(sib), RedBlackNode<T>.Colors.BLACK);
                        RotateLeft(parentOf(x));
                        x = root;
                    }
                }
                else
                {
                    RedBlackNode<T> sib = leftOf(parentOf(x));

                    if (colorOf(sib) == RedBlackNode<T>.Colors.RED)
                    {
                        setColor(sib, RedBlackNode<T>.Colors.BLACK);
                        setColor(parentOf(x), RedBlackNode<T>.Colors.RED);
                        RotateRight(parentOf(x));
                        sib = leftOf(parentOf(x));
                    }

                    if (colorOf(rightOf(sib)) == RedBlackNode<T>.Colors.BLACK &&
                        colorOf(leftOf(sib)) == RedBlackNode<T>.Colors.BLACK)
                    {
                        setColor(sib, RedBlackNode<T>.Colors.RED);
                        x = parentOf(x);
                    }
                    else
                    {
                        if (colorOf(leftOf(sib)) == RedBlackNode<T>.Colors.BLACK)
                        {
                            setColor(rightOf(sib), RedBlackNode<T>.Colors.BLACK);
                            setColor(sib, RedBlackNode<T>.Colors.RED);
                            RotateLeft(sib);
                            sib = leftOf(parentOf(x));
                        }
                        setColor(sib, colorOf(parentOf(x)));
                        setColor(parentOf(x), RedBlackNode<T>.Colors.BLACK);
                        setColor(leftOf(sib), RedBlackNode<T>.Colors.BLACK);
                        RotateRight(parentOf(x));
                        x = root;
                    }
                }
            }

            setColor(x, RedBlackNode<T>.Colors.BLACK);
        }

        ///<summary>
        /// RotateLeft
        /// Rebalance the tree by rotating the nodes to the left
        ///</summary>
        public void RotateLeft(RedBlackNode<T> n)
        {
            RedBlackNode<T> r = n.Right;
            n.Right = r.Left;
            if (r.Left != null)
                r.Left.Parent = n;
            r.Parent = n.Parent;

            if (n.Parent == null)
                root = r;
            else if (n.Parent.Left == n)
                n.Parent.Left = r;
            else
                n.Parent.Right = r;

            r.Left   = n;
            n.Parent = r;
        }

        ///<summary>
        /// RotateRight
        /// Rebalance the tree by rotating the nodes to the right
        ///</summary>
        public void RotateRight(RedBlackNode<T> n)
        {
            RedBlackNode<T> l = n.Left;
            n.Left = l.Right;
            if (l.Right != null)
                l.Right.Parent = n;
            l.Parent = n.Parent;

            if (n.Parent == null)
                root = l;
            else if (n.Parent.Right == n)
                n.Parent.Right = l;
            else
                n.Parent.Left = l;

            l.Right = n;
            n.Parent = l;
        }

        /// <summary>
        /// Realiza un intercambio de nodos entre el nodo X y el Y, esto se podria hacer con
        /// copia de valores, pero a la solucion hibrida de hashMap con apuntadores a nodos 
        /// no le serviria la copia de valores
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void swapPosition(RedBlackNode<T> x, RedBlackNode<T> y)
        {
            RedBlackNode<T> px = x.Parent;
            RedBlackNode<T> lx = x.Left;
            RedBlackNode<T> rx = x.Right;
            RedBlackNode<T> py = y.Parent;
            RedBlackNode<T> ly = y.Left;
            RedBlackNode<T> ry = y.Right;
            bool xWasLeftChild = (px != null) && (x == px.Left);
            bool yWasLeftChild = (py != null) && (y == py.Left);

            if (x == py)
            {
                x.Parent = y;
                if (yWasLeftChild)
                {
                    y.Left = x;
                    y.Right = rx;
                }
                else
                {
                    y.Right = x;
                    y.Left = lx;
                }
            }
            else
            {
                x.Parent = py;
                if (py != null)
                {
                    if (yWasLeftChild)
                        py.Left = x;
                    else
                        py.Right = x;
                }
                y.Left = lx;
                y.Right = rx;
            }

            if (y == px)
            {
                y.Parent = x;
                if (xWasLeftChild)
                {
                    x.Left = y;
                    x.Right = ry;
                }
                else
                {
                    x.Right = y;
                    x.Left = ly;
                }
            }
            else
            {
                y.Parent = px;
                if (px != null)
                {
                    if (xWasLeftChild)
                        px.Left = y;
                    else
                        px.Right = y;
                }
                x.Left = ly;
                x.Right = ry;
            }

            if (x.Left != null)
                x.Left.Parent = x;
            if (x.Right != null)
                x.Right.Parent = x;
            if (y.Left != null)
                y.Left.Parent = y;
            if (y.Right != null)
                y.Right.Parent = y;

            RedBlackNode<T>.Colors color = x.Color;
            x.Color = y.Color;
            y.Color = color;

            if (root == x)
                root = y;
            else if (root == y)
                root = x;
        }

        #endregion
        public override string ToString()
        {
            return inorden(root);
        }

        public string inorden(RedBlackNode<T> node)
        {
            string s = "";
            if (node != null)
            {
                s += inorden(node.Left);
                s += node.ToString() + " // ";
                s += inorden(node.Right);
            }
            return s;
        }

    }
}
