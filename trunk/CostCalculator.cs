using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;

namespace SolucionAlumno
{
	/// <summary>
	/// Encargado de obtener el calculador de costo.
	/// </summary>
	class CostCalculator {

        public enum CalculationType
        {
            CostBestFit,
            CostEuclidian,
            CostStadistic
        }

		private CostCalculatorStrategy internalCostCalculator;

		public CostCalculator(CalculationType type){
            internalCostCalculator = this.getCostCalculator(type);
		}

        private CostCalculatorStrategy getCostCalculator(CalculationType type)
        {
            switch (type)
            {
                case CalculationType.CostBestFit: return new CostBestFit();
                case CalculationType.CostEuclidian: return new CostEuclidian();
                case CalculationType.CostStadistic: return new CostStadistic();
                default: throw new ArgumentException("Unexpected CalculationType");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="actual"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public int aproximateMove(Point start, Point actual, Point goal)
        {
            return this.internalCostCalculator.aproximateMove(start, actual, goal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previousNode"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public int move(Node previousNode, Node actual)
        {
            return this.internalCostCalculator.move(previousNode, actual);
        }
	}
}