namespace SolucionAlumno {
	/// <summary>
	/// This class is configured with a ConcreteStrategy object, maintains a reference
	/// to a Strategy object and may define an interface that lets Strategy access its
	/// data.
	/// </summary>
	public class CostCalculator {

        public enum CalculationType : byte
        {
            CostBestFit,
            CostEuclidian,
            CostStadistic
        }

		public CostCalculatorStrategy m_CostCalculatorStrategy;

		public CostCalculator(CalculationType type){

		}

        private CostCalculatorStrategy GetCalculation(CalculationType type)
        {
            switch (type)
            {
                case CalculationType.CostBestFit: return new CostBestFit();
                case CalculationType.CostEuclidian: return new CostEuclidian();
                case CalculationType.CostStadistic: return new CostStadistic();
                default: throw new ArgumentException("Unexpected CalculationType");
            }
        }

		public virtual void Dispose(){

		}

		public void ContextInterface(){

		}

	}//end Context

}//end namespace Sistema