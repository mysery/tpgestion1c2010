using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;

namespace SolucionAlumno
{
    public class SolucionAlumno_v14 : ISolucion
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
            return "Solucion_v1.4, A* Basico, Con Hibrido de HashTable y Arbol Rojo-Negro para abiertos.";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new HashTableWithRedBlackTree(), new Hashtable()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesHash(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas), new CostCalculator(CostCalculator.CalculationType.CostBestFit));
        }

    }
}
