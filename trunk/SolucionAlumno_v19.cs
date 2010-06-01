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
            return "Solucion_v1.9, A* Basico, FibbonacciHeap para abiertos. H(x)= DiagonalSorcut + TieBreaks + Prueba y error. +Optimisacion.";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return cfa.ConnectionFind(ast,
                                      listaCheckPoint, 
                                      mapaDeCostos, 
                                      pph.processZones(zonasProhibidas),
                                      cc);
        }
        //Se definen estaticas las siguentes para no realizar instancia en cada ejecucion del algoritmo.
        private static ConnectionFindAbstraction cfa = new ConnectionFindAbstraction();
        private static FibonacciHeap<Node> fb = new FibonacciHeap<Node>();
        private static Hashtable ht = new Hashtable();
        private static PreProcesingZonesHash pph = new PreProcesingZonesHash();
        private static AStar ast = new AStar(fb, ht);
        private static CostCalculator cc = new CostCalculator(CostCalculator.CalculationType.CostBestFit);
    }
}
