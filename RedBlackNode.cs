using System;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    /// <summary>
    /// Nodo de un arbol RedBlack
    /// </summary>
    public class RedBlackNode<T> where T: IComparable
	{
        public enum Colors: byte
        {
            RED,
            BLACK
        }
        //Contiene items con la misma key que el nodo (no hay nodos repetidos en el arbol)
        private SortedList overflow;
        //vinculo con el padre en el cual fue agregado como overflow.
        private RedBlackNode<T> overflowParent;
        private bool isOverflow = false;
		// color - used to balance the tree
        private Colors color;
		// left node 
		private RedBlackNode<T> rbnLeft;
		// right node 
		private RedBlackNode<T> rbnRight;
        // parent node 
        private RedBlackNode<T> rbnParent;

        private T value;

        public RedBlackNode()
        {
            this.Color = Colors.RED;
        }

        public RedBlackNode(T value)
        {
            this.value = value;
            this.Color = Colors.RED;
        }

        public virtual T Value
        {
            get { return value; }
            set { this.value = value; }
        }
        
        public virtual bool IsOverflow
        {
            get { return isOverflow; }
        }

        public virtual RedBlackNode<T> OverflowParent
        {
            get { return overflowParent; }
            set { this.overflowParent = value; }
        }

		///<summary>
		///Color
		///</summary>
        public Colors Color
		{
			get { return color; }
			set { color = value; }
		}

		/// <summary>
        /// Left child get and set
		/// </summary>
		public RedBlackNode<T> Left
		{
			get { return rbnLeft; }
			set { rbnLeft = value; }
		}

		///<summary>
        /// Left child get and set
		///</summary>
		public RedBlackNode<T> Right
		{
			get { return rbnRight; }
			set { rbnRight = value; }
		}
        /// <summary>
        /// Parent
        /// </summary>
        public RedBlackNode<T> Parent
        {
            get { return rbnParent; }
            set { rbnParent = value; }
        }

        public RedBlackNode<T> getSuccessor()
        {
            //Busca el menor de los mayores
            if (this.Right != null)
            {
                RedBlackNode<T> n = this.Right;
                while (n.Left != null)
                    n = n.Left;
                return n;
            }
            //busca el mayor (esto solo si no tiene hijos a la derecha.)
            RedBlackNode<T> p = this.Parent;
            RedBlackNode<T> ch = this;
            while (p != null && ch == p.Right)
            {
                ch = p;
                p = p.Parent;
            }
            return p;
        }

        public void addOverflowItem(RedBlackNode<T> rbNode)
        {
            if (this.overflow == null)
                this.overflow = new SortedList();
            rbNode.isOverflow = true;
            rbNode.OverflowParent = this;
            this.overflow.Add(rbNode.Value.GetHashCode(), rbNode);
        }

        public RedBlackNode<T> getOverflowItem(T value)
        {
            if (this.overflow != null)
                return this.overflow[value.GetHashCode()] as RedBlackNode<T>;
            return null;
        }

        public RedBlackNode<T> getOverflowItem()
        {
            if (this.overflow != null)
                return this.overflow.GetByIndex(0) as RedBlackNode<T>;
            return null;
        }

        public void removeOverflowItem(T value)
        {
            if (this.overflow != null)
            {
                //Si el item a remover es el mismo hay que hacer un biribiri
                if (this.Value.Equals(value))
                {   //Tomo el primer valor y lo remplazo.
                    this.Value = (this.overflow.GetByIndex(0) as RedBlackNode<T>).Value;
                    this.overflow.Remove(this.Value.GetHashCode());                    
                } else 
                    this.overflow.Remove(value.GetHashCode());

                if (this.overflow.Count == 0)
                    this.overflow = null;
            }
        }

        public bool hasOverflow()
        {
            return this.overflow != null;
        }

        public override string ToString()
        {
            return this.Value + ((this.overflow != null) ? " - O" : "");
        }

        public override bool Equals(object o)
        {
            RedBlackNode<T> n = o as RedBlackNode<T>;
            if (n == null)
            {
                return false;
            }
            return (this.Value.GetHashCode() == n.Value.GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        //add this code to class ThreeDPoint as defined previously
        //
        public static bool operator ==(RedBlackNode<T> a, RedBlackNode<T> b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(RedBlackNode<T> a, RedBlackNode<T> b)
        {
            return !(a == b);
        }
    }
}
