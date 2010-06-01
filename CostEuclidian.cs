using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BibliotecaComun;

namespace SolucionAlumno
{
	/// <summary>
	/// Calcula el costo euclidiano.
	/// </summary>
    class CostEuclidian : CostCalculatorStrategy
    {

		/// <summary>
        /// Calcula el costo aproximado (heuristica) de movimiento hasta el nodo final.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="actual"></param>
		/// <param name="goal"></param>
		/// <returns></returns>
        public override int aproximateMove(Point start, Point actual, Point goal)
        {
            //Distancia de Euclidean (Lenta por el SQRT)
            double value = 0;
            value = 241 * Math.Sqrt((Math.Pow((actual.X - goal.X), 2) + Math.Pow((actual.Y - goal.Y), 2)));
            //TIE BREAKERS!!!
            value = (value * 1.078333);// 1.0 + RECT * DIAGONAL / 2 / 360000));
            int dx1 = actual.X - goal.X;
            int dy1 = actual.Y - goal.Y;
            int dx2 = start.X - goal.X;
            int dy2 = start.Y - goal.Y;
            int cross = Math.Abs(dx1 * dy2 * 100 - dx2 * dy1 * 100);
            value += (cross * 0.001);
            return (int)Math.Truncate(value);
        }
    }
}