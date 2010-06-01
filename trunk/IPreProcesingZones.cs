using System;
using BibliotecaComun;
using System.Collections.Generic;

namespace SolucionAlumno
{
    interface IPreProcesingZones
    {
        IPreProcesingZones processZones(List<ZonaProhibida> zonasProhibidas);
        ZonaProhibida this[int x, int y] { get; }
    }
}
