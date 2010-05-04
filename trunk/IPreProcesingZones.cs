using System;
using BibliotecaComun;
using System.Collections.Generic;

namespace SolucionAlumno
{
    interface IPreProcesingZones
    {
        void processZones(List<ZonaProhibida> zonasProhibidas);
        ZonaProhibida this[int x, int y] { get; }
    }
}
