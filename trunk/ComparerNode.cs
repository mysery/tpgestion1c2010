using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SolucionAlumno
{
    class ComparerNode : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if ((int)x < (int)y)
            {
                return -1;
            }
            else if ((int)x == (int)y)
            {
                return 1;
            }
            return 1;
        }

        #endregion
    }
}
