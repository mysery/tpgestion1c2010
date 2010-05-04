using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;
using System.Drawing;

namespace SolucionAlumno
{
    class PreProcesingZonesHash : IPreProcesingZones
    {
        private Hashtable hashZones;

        public PreProcesingZonesHash(int width, int height, List<ZonaProhibida> zonasProhibidas)
        {
            hashZones = new Hashtable(zonasProhibidas.Count);
            this.processZones(zonasProhibidas);
        }

        public void processZones(List<ZonaProhibida> zonasProhibidas)
        {
            foreach (ZonaProhibida zonaProhibida in zonasProhibidas)
            {
                zonaProhibida.GetHashCode();
                for (int i = zonaProhibida.X; i <= (zonaProhibida.X + zonaProhibida.Width); i++)
                    for (int j = zonaProhibida.Y; j <= (zonaProhibida.Y + zonaProhibida.Height); j++)
                    { 
                        if (!hashZones.Contains((i * 10000 + j)))
                            hashZones.Add((i * 10000 + j), zonaProhibida);
                    }
            }
        }

        public ZonaProhibida this[int x, int y]
        { 
            get {
                return hashZones[(x * 10000 + y)] as ZonaProhibida;
                }
        }
    }
}
