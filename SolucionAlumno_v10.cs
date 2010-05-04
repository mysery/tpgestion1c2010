using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    public class SolucionAlumno_v10 : ISolucion
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
            return "Solucion_v1.0, A* Basico, con un BinaryTree";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            return new ConnectionFindAbstraction().ConnectionFind(new AStar(new BinaryTree<Node>(), new BinaryTree<Node>()), listaCheckPoint, mapaDeCostos, new PreProcesingZonesMatrix(mapaDeCostos.getDimensiones().Width, mapaDeCostos.getDimensiones().Height, zonasProhibidas));
        }

    }
}
