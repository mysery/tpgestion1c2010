using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
    class CostSimpleTieBreak: CostCalculatorStrategy
    {
        public override int aproximateMove(Point start, Point actual, Point goal)
        {
            int value = 0;

            int rectAproxCost = RECT;
            int diagonalAproxCost = DIAGONAL;
            int diagonal = Math.Min(Math.Abs(actual.X - goal.X), Math.Abs(actual.Y - goal.Y));
            int direct = Math.Abs(actual.X - goal.X) + Math.Abs(actual.Y - goal.Y);
            value += diagonalAproxCost * diagonal + rectAproxCost * (direct - 2 * diagonal);
            
            //TIE BREAKERS!!!
            value = (int)Math.Truncate(value * (1.270));//241));//(RECT + DIAGONAL)/2 + 0.5 * COST_PONDERATION) / 1500));
            return value;
        }
    }
}
