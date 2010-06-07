using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Collections;

namespace SolucionAlumno
{
    public class SolucionAlumno_v18 : ISolucion
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
            return "Solucion_v1.8, A* Basico, FibbonacciHeap para abiertos. H(x)= DiagonalShorcut + Estadistico";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new FibonacciHeap<Node>(), new Hashtable()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesHash(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas), new CostCalculator(CostCalculator.CalculationType.CostStadistic));
        }

    }
}
