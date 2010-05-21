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
    class CostEuclidian
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
         * Constante con el indice para darle al costo una importancia suficiente como para elejir un buen camino.
         */
        private const int COST_FIX = 300;

        private int totalCostCounted = 0;
        private double totalCostSumed = 0;

		/**
		*	Instancia del singleton CostCalculator.
		*/
        private static CostEuclidian instance = null;

		/**
		*	Consturctor privado para no permitir instanciacion.
		*/
        private CostEuclidian() { }

		/**
		*	Obtencion de instancia de CostCalculator.
		*/
        public static CostEuclidian Instance
        {
            get
            {
                if (instance == null) {
                    instance = new CostEuclidian();
                }
                return instance;
            }
        }

		/**
		*	Calcula el costo de movimiento.
		*/
        public int move(Node previousNode, Node actual)
        {
            double costToFix = actual.Costo.Valor;
            //Guardo un conteo de costos para poder realizar una estadistica.
            totalCostSumed += costToFix;
            totalCostCounted++;
            int precalculed = previousNode.GValue;
            if (previousNode.Point.X == actual.Point.X || previousNode.Point.Y == actual.Point.Y)
            {
                precalculed += (int)Math.Truncate((RECT - COST_FIX * costToFix) + RECT);
            } 
            else
            {
                precalculed += (int)Math.Truncate((DIAGONAL - COST_FIX * costToFix) + DIAGONAL);
            }
            return Math.Abs(precalculed);
        }
        
		/**
		*	Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		*/
        public int aproximateMove(Point start, Point actual, Point goal)
        {
            
            int value = 0;
            //return 0;
            
            int rectAproxCost = RECT; //this.getAproximateCostTerain(RECT);
            int diagonalAproxCost = DIAGONAL; //this.getAproximateCostTerain(DIAGONAL);
            int diagonal = Math.Min(Math.Abs(actual.X - goal.X), Math.Abs(actual.Y - goal.Y));
            int direct = Math.Abs(actual.X - goal.X) + Math.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);
            //Distancia de Euclidean (Lenta por el SQRT)
            //value += (int)Math.Truncate(RECT * Math.Sqrt((Math.Pow((actual.X - goal.X), 2) + Math.Pow((actual.Y - goal.Y), 2))));
            //value = this.getAproximateCostTerain(value);
            value = (int)Math.Truncate(value * (1.0 + RECT * DIAGONAL / 2 /36000));
            int dx1 = actual.X - goal.X;
            int dy1 = actual.Y - goal.Y;
            int dx2 = start.X - goal.X;
            int dy2 = start.Y - goal.Y;
            int cross = Math.Abs(dx1 * dy2 * 100 - dx2 * dy1 * 100);
            value += (int)Math.Truncate(cross * 0.001);
            return value;
        }

        private int getAproximateCostTerain(int moveCost)
        {
            return (int)Math.Truncate(moveCost - ((COST_FIX * totalCostSumed) / totalCostCounted)) + moveCost;
        }
    }
}

namespace SolucionAlumno {
	/// <summary>
	/// Clase utilizada para abstraer los calculos de costo en movimiento y terreno.
	/// </summary>
	public class CostEuclidian : CostCalculatorStrategy {

		/// <summary>
		/// Constante con el indice para darle al costo una importancia suficiente como
		/// para elejir un buen camino.
		/// </summary>
		private const int COST_FIX = 300;
		/// <summary>
		/// Constante con el costo de moviento diagonal, multiplicado por 100 y truncado
		/// para no manerjar puntos flotantes.
		/// </summary>
		private const int DIAGONAL = 282;
		/// <summary>
		/// Constante con el costo de moviento recto, multiplicado por 100 para no manerjar
		/// puntos flotantes.
		/// </summary>
		private const int RECT = 200;
		private int totalCostCounted = 0;
		private double totalCostSumed = 0;

		public CostEuclidian(){

		}

		~CostEuclidian(){

		}

		public override void Dispose(){

		}

		/// <summary>
		/// Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="actual"></param>
		/// <param name="goal"></param>
		public int aproximateMove(Point start, Point actual, Point goal){

			return 0;
		}

		/// 
		/// <param name="moveCost"></param>
		private int getAproximateCostTerain(int moveCost){

			return 0;
		}

		/// <summary>
		/// Calcula el costo de movimiento.
		/// </summary>
		/// <param name="previousNode"></param>
		/// <param name="actual"></param>
		public int move(Node previousNode, Node actual){

			return 0;
		}

	}//end CostCalculator

}//end namespace Sistema
