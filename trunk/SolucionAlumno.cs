using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    public class SolucionAlumno : ISolucion
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
            return "Solucion_v1.0";
        }

        public ListaConexiones buscarConexiones(ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, List<ZonaProhibida> zonasProhibidas)
        {
            int cantCheckpoints = listaCheckPoint.Count - 1;
            AStar aStar = new AStar();
            ListaConexiones listaConexiones = new ListaConexiones();
            for (int i = 0; i < cantCheckpoints; i++) {
                CheckPoint start = listaCheckPoint[i];
                CheckPoint goal = listaCheckPoint[i + 1];
                Conexion conexion = aStar.buscarCamino(start, goal, mapaDeCostos);
                listaConexiones.Add(conexion);
            }
            return listaConexiones;
        }

    }
}
