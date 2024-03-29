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
        private const int PRIME = 10000;

        public PreProcesingZonesHash()
        {
            hashZones = new Hashtable();
        }
        
        public PreProcesingZonesHash(int width, int height, List<ZonaProhibida> zonasProhibidas)
        {
            hashZones = new Hashtable();
            this.processZones(zonasProhibidas);
        }

        public IPreProcesingZones processZones(List<ZonaProhibida> zonasProhibidas)
        {
           
            foreach (ZonaProhibida zonaProhibida in zonasProhibidas)
            {
                zonaProhibida.GetHashCode();
                for (int i = zonaProhibida.X; i <= (zonaProhibida.X + zonaProhibida.Width); i++)
                    for (int j = zonaProhibida.Y; j <= (zonaProhibida.Y + zonaProhibida.Height); j++)
                    {
                        if (!hashZones.Contains((i << PRIME | j)))
                            hashZones.Add((i << PRIME | j), zonaProhibida);
                    }
            }
            return this;
        }

        public ZonaProhibida this[int x, int y]
        { 
            get {
                return hashZones[(x << PRIME | y)] as ZonaProhibida;
                }
        }
    }
}
