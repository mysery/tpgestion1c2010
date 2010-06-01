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
            double value = 0;

            int rectAproxCost = RECT;
            int diagonalAproxCost = DIAGONAL;
            int diagonal = Math.Min(MyMath.Abs(actual.X - goal.X), MyMath.Abs(actual.Y - goal.Y));
            int direct = MyMath.Abs(actual.X - goal.X) + MyMath.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);
            
            //TIE BREAKERS!!!
            value *= 1.078333;// 1.0 + RECT * DIAGONAL / 2 / 360000));
            int dx1 = actual.X - goal.X;
            int dy1 = actual.Y - goal.Y;
            int dx2 = start.X - goal.X;
            int dy2 = start.Y - goal.Y;
            int cross = dx1 * dy2 * RECT - dx2 * dy1 * RECT;
            //La operacion (x ^ (x >> 31)) - (x >> 31); remplaza a Math.Abs 
            value += MyMath.Abs(cross) * 0.001;
            return (int)value;
		}
	}

}