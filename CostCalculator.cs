using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
	/**
	*	Clase utilizada para abstraer los calculos de costo en movimiento y terreno.
	*/
    class CostCalculator
    {
		/**
		*	Constante con el costo de moviento recto, multiplicado por 100 para no manerjar puntos flotantes. 
		*/
		private const int RECT = 200;
		/** 
		*	Constante con el costo de moviento diagonal, multiplicado por 100 y truncado para no manerjar puntos flotantes. 
		*/
        private const int DIAGONAL = 282;

		/**
		*	Instancia del singleton CostCalculator.
		*/
        private static CostCalculator instance = null;

		/**
		*	Consturctor privado para no permitir instanciacion.
		*/
        private CostCalculator() { }

		/**
		*	Obtencion de instancia de CostCalculator.
		*/
        public static CostCalculator Instance
        {
            get
            {
                if (instance == null) {
                    instance = new CostCalculator();
                }
                return instance;
            }
        }

		/**
		*	Calcula el costo de movimiento.
		*/
        public int move(Point actual, Point next)
        {
            if (actual.X == next.X || actual.Y == next.Y)
            {
                return RECT;
            }
            return DIAGONAL;
        }
        
		/**
		*	Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		*/
        public int aproximateMove(Point actual, Point goal)
        {
            return (int) Math.Round(100 * Math.Sqrt(((actual.X - goal.X) ^ 2 + (actual.Y - goal.Y) ^ 2)), 0);
        }

    }
}
