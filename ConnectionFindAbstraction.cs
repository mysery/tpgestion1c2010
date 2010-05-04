using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;

namespace SolucionAlumno
{
    class ConnectionFindAbstraction
    {
        /**
         * Abstraccion para obtener un camino para un algoritmo que implementa IAlgorithm.
         */
        public ListaConexiones ConnectionFind(IAlgorithm algorithm, ListaCheckPoint listaCheckPoint, MapaDeCostos mapaDeCostos, IPreProcesingZones zonasProhibidas)
        {
            int cantCheckpoints = listaCheckPoint.Count - 1;
            ListaConexiones listaConexiones = new ListaConexiones();
            for (int i = 0; i < cantCheckpoints; i++)
            {
                CheckPoint start = listaCheckPoint[i];
                //Che esto esta bien??? me hace ruido :P
                CheckPoint goal = listaCheckPoint[i + 1];
                Logger.appendInfo("Buscando conexion entre " + start.Id + " y " + goal.Id);
                Conexion conexion = algorithm.pathFind(start, goal, mapaDeCostos, zonasProhibidas);
                if (conexion == null)
                {
                    Logger.appendError("No se encontro camino entre " + start.Id + " y " + goal.Id);
                    throw new Exception("No se encontro camino entre " + start.Id + " y " + goal.Id);
                }
                Logger.appendInfo("Se encontro el camino " + start.Id + " y " + goal.Id);
                Logger.appendInfo("Con un costo de " + conexion.GetCostoCamino() + " y largo de " + conexion.GetDistanciaCamino());
                listaConexiones.Add(conexion);
            }
            return listaConexiones;
        }
    }
}

