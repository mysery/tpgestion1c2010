using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;

namespace SolucionAlumno
{
    public class SolucionAlumno_v19 : ISolucion
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
            return "Solucion_v1.9, A* Basico, FibbonacciHeap para abiertos. H(x)= 0";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new FibonacciHeap<Node>(), new Hashtable()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesHash(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas), new CostCalculator(CostCalculator.CalculationType.CostZero));
        }

    }
}
