using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    interface IAlgorithm
    {
        /**
         * Busca un camino entre startCheckpoint y goalCheckpoint teniendo en cuenta los costos del mapaDeCostos.
         */
        Conexion pathFind(CheckPoint startCheckpoint, CheckPoint goalCheckpoint, MapaDeCostos mapaDeCostos, PreProcesingZones zonasProhibidas);
    }
}
