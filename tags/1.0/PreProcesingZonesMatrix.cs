using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Drawing;

namespace SolucionAlumno
{
    class PreProcesingZonesMatrix : IPreProcesingZones
    {
        private ZonaProhibida[,] matrixZonas;

        public PreProcesingZonesMatrix(int width, int height, List<ZonaProhibida> zonasProhibidas)
        {
           matrixZonas = new ZonaProhibida[width, height];
           this.processZones(zonasProhibidas);
        }

        public virtual IPreProcesingZones processZones(List<ZonaProhibida> zonasProhibidas)
        {
            foreach (ZonaProhibida zonaProhibida in zonasProhibidas)
            {
                zonaProhibida.GetHashCode();
                Size s = new Size(zonaProhibida.Width, zonaProhibida.Height);
                for (int i = zonaProhibida.X; i <= (zonaProhibida.X + zonaProhibida.Width); i++)
                    for (int j = zonaProhibida.Y; j <= (zonaProhibida.Y + zonaProhibida.Height); j++)
                        matrixZonas[i, j] = zonaProhibida;
            }
            return this;
        }

        public virtual ZonaProhibida this[int x, int y]
        { 
            get { 
                return matrixZonas[x, y];
                }
        }
    }
}
