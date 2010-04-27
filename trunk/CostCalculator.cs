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
        private double totalCostSumed = 0;

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
        public int move(Node previousNode, Node actual, MapaDeCostos mapaDeCostos)
        {
            double costToFix = actual.Costo.Valor;//(int)Math.Truncate(1000 * actual.Costo.Valor);
            totalCostSumed += costToFix;
            totalCostCounted++;
            int precalculed = previousNode.GValue;// +costToFix;
            if (previousNode.Point.X == actual.Point.X || previousNode.Point.Y == actual.Point.Y)
            {
                precalculed += RECT + (int)Math.Truncate(100 * costToFix);
            } 
            else
            {
                precalculed += DIAGONAL + (int)Math.Truncate(100 * costToFix);
            }
            return precalculed;
        }
        
		/**
		*	Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		*/
        public int aproximateMove(Point actual, Point goal)
        {
            int value = 0;
            int rectAproxCost = RECT + this.getAproximateCostTerain(); // + (int)Math.Truncate(RECT * totalCostSumed / totalCostCounted);
            int diagonalAproxCost = DIAGONAL + this.getAproximateCostTerain(); // (int)Math.Truncate(DIAGONAL * totalCostSumed / totalCostCounted);
            int diagonal = Math.Min(Math.Abs(actual.X - goal.X), Math.Abs(actual.Y - goal.Y));
            int direct = Math.Abs(actual.X - goal.X) + Math.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);
            //Distancia de Euclidean (Lenta por el SQRT)
            //value += (int)Math.Truncate(RECT * Math.Sqrt((Math.Pow((actual.X - goal.X), 2) + Math.Pow((actual.Y - goal.Y), 2))));
            //value = (value * (totalCostSumed / totalCostCounted))/100;
            return value;
        }

        private int getAproximateCostTerain()
        {
            return 0;// (int)Math.Truncate(100 * totalCostSumed / totalCostCounted);
        }
    }
}
