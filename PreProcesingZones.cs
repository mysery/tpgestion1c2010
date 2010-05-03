using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    class PreProcesingZones
    {
        private ZonaProhibida[,] matrixZonas;

        public PreProcesingZones(int width, int height, List<ZonaProhibida> zonasProhibidas)
        {
           matrixZonas = new ZonaProhibida[width, height];
           this.processZones(zonasProhibidas);
        }

        public void processZones(List<ZonaProhibida> zonasProhibidas)
        {
            foreach (ZonaProhibida zonaProhibida in zonasProhibidas)
            {
                for (int i = zonaProhibida.X; i <= (zonaProhibida.X + zonaProhibida.Width); i++)
                    for (int j = zonaProhibida.Y; j <= (zonaProhibida.Y + zonaProhibida.Height); j++)
                        matrixZonas[i, j] = zonaProhibida;
            }
        }

        public ZonaProhibida this[int x, int y] { 
            get { 
                return matrixZonas[x, y];
                }
        }
    }
}
