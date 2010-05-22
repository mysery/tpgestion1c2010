using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;

namespace SolucionAlumno
{
    public class SolucionAlumno_v12 : ISolucion
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
            return "Solucion_v1.2, A* Basico, Con HashTable y un BinaryTree";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new HashTableWithTree(), new Hashtable()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesMatrix(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas), new CostCalculator(CostCalculator.CalculationType.CostBestFit));
        }

    }
}
