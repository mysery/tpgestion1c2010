using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SolucionAlumno
{
    class CostWithCrossProduct: CostCalculatorStrategy
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
            int dx1 = (actual.X - goal.X);
            int dy1 = (actual.Y - goal.Y);
            int dx2 = (start.X - goal.X);
            int dy2 = (start.Y - goal.Y);
            int cross = Math.Abs(dx1 * dy2 * RECT - dx2 * dy1 * RECT);
            value += (int)Math.Truncate(cross * 0.001);
        
            return value;
        }
    }
}
