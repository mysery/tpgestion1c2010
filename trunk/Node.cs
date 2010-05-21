using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BibliotecaComun;
using System.Collections;

namespace SolucionAlumno
{
	/**
	*	Clase que representa un nodo en el mapa.
	*/
    class Node : IComparable
    {
		/** 
         * Nodo padre 
         */
        private Node parent;
		/** 
         * punto en el mapa al que pertenece dicho nodo 
         */
        private Point point;
		/** 
         * Costo en dicho nodo 
         */
        private Costo costo;
		/** 
         * Valor de la funcion f(x) Utilizada para el algoritmo A*, f = g + h 
         */
        private int fValue;
		/** 
         * Valor de la funcion g(x) Utilizada para el algoritmo A*, Representa el costo por moverse a dicha posicion. 
         */
        private int gValue;
		/** 
         * Valor de la funcion f(x) Utilizada para el algoritmo A*, Representa el costo aproximado para llegar  al destino. 
         */
        private int hValue;

		/**
		* Constructor por con costo y punto
		* @param costo del nodo creado.
		* @param point del nodo creado.
		*/
        public Node(Costo costo, Point point) {
            this.costo = costo;
            this.point = point;
        }

		/**
		* Calcula el costo de movimiento para el Nodo
		*/
        public void calculateCost(Point start, Node previousPoint, Point goal) {
            this.gValue = CostEuclidian.Instance.move(previousPoint, this);
            this.hValue = CostEuclidian.Instance.aproximateMove(start, this.Point, goal);
            fValue = gValue + hValue;
        }
        
		/**
		* Obtiene la lista de Nodos adyasentes en el mapa.
		*/
        public List<Node> getAdjacent(MapaDeCostos mapaDeCostos, Hashtable closeList, IPreProcesingZones zonasProhibidas)
        {
            List<Node> nodosAdyacentes = new List<Node>();

            for (int x = this.Point.X - 1; x <= this.Point.X + 1; x++)
            {
                for (int y = this.Point.Y - 1; y <= this.Point.Y + 1; y++)
                {
                    if (!((x == this.Point.X) && (y == this.Point.Y)))
                    {
                        Point point = new Point();
                        point.X = x;
                        point.Y = y;                        
                        if (mapaDeCostos.verificarPosicion(point))
                        {
                            Node nodoAdyacente = new Node(mapaDeCostos.getCostoPosicion(point), point);
                            if (nodoAdyacente.Costo.esTransitable() &&
                                !closeList.Contains(nodoAdyacente.GetHashCode()))
                            {
                                ZonaProhibida zonaProhibida = zonasProhibidas[nodoAdyacente.Point.X, nodoAdyacente.Point.Y];
                                if (zonaProhibida != null)// && mapaDeCostos.verificarZonaProhibida(nodoAdyacente.Point, zonaProhibida))
                                {
                                    continue;
                                }
                                nodosAdyacentes.Add(nodoAdyacente);
                            }
                        }
                    }
                }
            }
            return nodosAdyacentes;
        }

		/**
		* Get y Set de parent
		*/
        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }
		
		/**
		* Get y Set de point
		*/
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
		
		/**
		* Get y Set de costo
		*/
        public Costo Costo
        {
            get { return costo; }
            set { costo = value; }
        }

		/**
		* Get de fValue
		*/
        public int FValue
        {
            get { return fValue; }
            //set { fValue = value; }
        }

		/**
		* Get de gValue
		*/
        public int GValue
        {
            get { return gValue; }
            //set { gValue = value; }
        }

		/**
		* Get de hValue
		*/
        public int HValue
        {
            get { return hValue; }
            //set { hValue = value; }
        }

		/**
		*	Implementacion de la IComparable.
		*/
		public int CompareTo(object obj) {
            if (this.fValue < ((Node)obj).fValue) {
                return -1;
            } else if(this.fValue == ((Node)obj).fValue) { 
                return 0;
            }
            return 1;
        }

        /**
		*	Implementacion de Equals.
		*/
        public override bool Equals(object obj)
        {
            if (obj is Node)
            {
                return this.point.Equals(((Node) obj).Point);
            }
            return false;
        }

        /**
		*	Implementacion de GetHastCode.
		*/
        public override int GetHashCode()
        {
            return this.point.X << 10000 | this.point.Y;
        }

        public override string ToString()
        {
            return this.Point.ToString();
        }


    }
}

