using System;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
	/// <summary>
	/// This class implements the algorithm using the Strategy interface.
	/// </summary>
	class CostBestFit : CostCalculatorStrategy {

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
            //return 0;

            int rectAproxCost = RECT;
            int diagonalAproxCost = DIAGONAL;
            int diagonal = Math.Min(Math.Abs(actual.X - goal.X), Math.Abs(actual.Y - goal.Y));
            int direct = Math.Abs(actual.X - goal.X) + Math.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);
            
            value = (int)Math.Truncate(value * (1.0 + RECT * DIAGONAL / 2 / 36000));
            int dx1 = actual.X - goal.X;
            int dy1 = actual.Y - goal.Y;
            int dx2 = start.X - goal.X;
            int dy2 = start.Y - goal.Y;
            int cross = Math.Abs(dx1 * dy2 * 100 - dx2 * dy1 * 100);
            value += (int)Math.Truncate(cross * 0.001);
            return value;
		}
	}

}