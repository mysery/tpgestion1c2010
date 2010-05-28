using System;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
	/// <summary>
	/// This class implements the algorithm using the Strategy interface.
	/// </summary>
	class CostStadistic : CostCalculatorStrategy {

        private static int totalCostCounted = 0;
        private static double totalCostSumed = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="actual"></param>
		/// <param name="goal"></param>
		/// <returns></returns>
        public override int aproximateMove(Point start, Point actual, Point goal)
        {
            int value = 0;
            int rectAproxCost = this.getAproximateCostTerain(RECT);
            int diagonalAproxCost = this.getAproximateCostTerain(DIAGONAL);
            int diagonal = Math.Min(Math.Abs(actual.X - goal.X), Math.Abs(actual.Y - goal.Y));
            int direct = Math.Abs(actual.X - goal.X) + Math.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);

            //TIE BREAKERS!!!
            value = (int)Math.Truncate(value * 1.275);//(1.0 + (((RECT + DIAGONAL) / 2 + (totalCostSumed / totalCostCounted) * COST_PONDERATION) / 1500)));

            return value;
		}

        public override int move(Node previousNode, Node actual)
        {
            totalCostSumed += (1 - actual.Costo.Valor);
            totalCostCounted++;
            return base.move(previousNode, actual);
        }

        private int getAproximateCostTerain(int moveCost)
        {
            return (int)Math.Truncate(moveCost * 0.5 * (totalCostSumed / totalCostCounted) + moveCost * 0.5);
        }
	}

}