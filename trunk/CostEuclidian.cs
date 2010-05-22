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
            return (int)Math.Truncate(RECT * Math.Sqrt((Math.Pow((actual.X - goal.X), 2) + Math.Pow((actual.Y - goal.Y), 2))));
        }
    }
}