using System;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
	/// <summary>
	/// interface definda para los costos.
	/// </summary>
	abstract class CostCalculatorStrategy {

        /// <summary>
        /// Constante con el indice para darle al costo una importancia suficiente como
        /// para elejir un buen camino. Ponderacion
        /// </summary>
        public const int COST_PONDERATION = 300;
        /// <summary>
        /// Constante con el costo de moviento diagonal, multiplicado por 100 y truncado
        /// para no manerjar puntos flotantes.
        /// </summary>
        public const int DIAGONAL = 282;
        /// <summary>
        /// Constante con el costo de moviento recto, multiplicado por 100 para no manerjar
        /// puntos flotantes.
        /// </summary>
        public const int RECT = 200;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="actual"></param>
		/// <param name="goal"></param>
		/// <returns></returns>
        public abstract int aproximateMove(Point start, Point actual, Point goal);

		/// <summary>
        /// Calcula el costo de movimiento.
		/// </summary>
		/// <param name="previousNode"></param>
		/// <param name="actual"></param>
		/// <returns></returns>
        public virtual int move(Node previousNode, Node actual)
        {
            double costToFix = actual.Costo.Valor;
            int precalculed;
            if (previousNode.Point.X == actual.Point.X || previousNode.Point.Y == actual.Point.Y)
            {
                precalculed = (int)((RECT - COST_PONDERATION * costToFix) + RECT);
                //(COST_PONDERATION * (1 - costToFix)
            }
            else
            {
                precalculed = (int)((DIAGONAL - COST_PONDERATION * costToFix) + DIAGONAL);
            }
            return previousNode.GValue + MyMath.Abs(precalculed);
        }
	}
}