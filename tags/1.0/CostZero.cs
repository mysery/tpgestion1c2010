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
    class CostZero: CostCalculatorStrategy
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
            return 0;
        }
    }
}