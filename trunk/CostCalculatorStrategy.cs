namespace SolucionAlumno
{
	/// <summary>
	/// This class declares an interface common to all supported algorithms. Context
	/// uses this interface to call the algorithm defined by a ConcreteStrategy.
	/// </summary>
	public abstract class CostCalculatorStrategy {

		/// 
		/// <param name="start"></param>
		/// <param name="actual"></param>
		/// <param name="goal"></param>
		public abstract int aproximateMove(Point start, Point actual, Point goal);

		/// 
		/// <param name="previousNode"></param>
		/// <param name="actual"></param>
		public abstract int move(Node previousNode, Node actual);
	}
}