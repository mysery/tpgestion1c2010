using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BibliotecaComun;

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
        
        private int totalCostCounted = 0;
        private int totalCostSumed = 0;

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
        public int move(Point actual, Point next, MapaDeCostos mapaDeCostos)
        {
            totalCostSumed += (int)Math.Truncate(100 * mapaDeCostos.getCostoPosicion(next).Valor);
            totalCostCounted++;
            if (actual.X == next.X || actual.Y == next.Y)
            {
                return RECT +(int)Math.Truncate(100 * mapaDeCostos.getCostoPosicion(next).Valor);
            }
            return DIAGONAL +(int)Math.Truncate(100 * mapaDeCostos.getCostoPosicion(next).Valor);
        }
        
		/**
		*	Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		*/
        public int aproximateMove(Point actual, Point goal)
        {
            int value = 0;
            value += (int)Math.Truncate(100 * Math.Sqrt((Math.Pow((actual.X - goal.X), 2) + Math.Pow((actual.Y - goal.Y), 2))));
            value += totalCostSumed / totalCostCounted;
            return value;
        }

    }
}
