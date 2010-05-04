using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    public class SolucionAlumno_v11 : ISolucion
    {

        public string curso()
        {
            return "K3021";
        }

        public int numeroGrupo()
        {
            return 1;
        }

        public string descripcion()
        {
            return "Solucion_v1.1, A* Basico, Con HashTable con una variable minimo.";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new HashTableWithMinimum(), new HashTableWithMinimum()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesMatrix(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas));
        }

    }
}
