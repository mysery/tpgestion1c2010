using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionAlumno
{
    class MyMath
    {
        /// <summary>
        /// La operacion (x ^ (x >> 31)) - (x >> 31); remplaza a Math.Abs 
        /// para nuestra aplicacion resulta mas peforfmante.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Abs(int x)
        {
            return (x ^ (x >> 31)) - (x >> 31);
        }
        
    }
}
