using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;
using System.Drawing;

namespace SolucionAlumno
{
    class PreProcesingZonesRTree : IPreProcesingZones
    {
        private RTree<ZonaProhibida> rtree;

        public PreProcesingZonesRTree()
        {
            rtree = new RTree<ZonaProhibida>();
        }

        public PreProcesingZonesRTree(List<ZonaProhibida> zonasProhibidas)
        {
            rtree = new RTree<ZonaProhibida>(zonasProhibidas.Count, 0);
            this.processZones(zonasProhibidas);
        }

        public IPreProcesingZones processZones(List<ZonaProhibida> zonasProhibidas)
        {
            foreach (ZonaProhibida zonaProhibida in zonasProhibidas)
            {
                rtree.Add(new RRectangle(zonaProhibida), zonaProhibida);                
            }
            return this;
        }

        public ZonaProhibida this[int x, int y]
        { 
            get {
                List<ZonaProhibida> l = rtree.Nearest(new RPoint(x, y), 0);
                if (l.Count > 0)
                    return l[0];
                return null;
                }
        }
    }
}
