using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BibliotecaComun;

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
		* Constructor por defecto
		*/
        public Node() { }

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
        public void calculateCost(Point nextPoint, Point goal) {
            this.gValue = CostCalculator.Instance.move(this.Point, nextPoint);
            this.hValue = CostCalculator.Instance.aproximateMove(this.Point, goal);
            fValue = gValue + hValue;
        }
        
		/**
		* Obtiene la lista de Nodos adyasentes en el mapa.
		*/
        public List<Node> getAdyacentes(){
        	//TODO Hacer el metodo
            return null;
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
            return this.point.Equals(obj);            
        }

        /**
		*	Implementacion de GetHastCode.
		*/
        public override int GetHashCode()
        {
            return this.point.GetHashCode();
        }
    }
}

