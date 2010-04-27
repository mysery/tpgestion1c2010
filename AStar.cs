using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaComun;
using System.Drawing;

namespace SolucionAlumno
{
    /**
     * Clase implementadora de A*
     */
    class AStar : IAlgorithm
    {
        private IOrderSerchStruct<Node> openStruct;
        private IOrderSerchStruct<Node> closeStruct;

        public AStar(IOrderSerchStruct<Node> pOpenStruct, IOrderSerchStruct<Node> pCloseStruct)
        {
            this.openStruct = pOpenStruct;
            this.closeStruct = pCloseStruct;
        }

        /**
         * Busca un camino entre startCheckpoint y goalCheckpoint teniendo en cuenta los costos del mapaDeCostos.
         * Implementando el algoritmo A*
         */
        public Conexion pathFind(CheckPoint startCheckpoint, CheckPoint goalCheckpoint, MapaDeCostos mapaDeCostos)
        {
            //Se limpian las estructuras.
            openStruct.Clear();
            closeStruct.Clear();
            
            Point start = startCheckpoint.Posicion;
            Point goal = goalCheckpoint.Posicion;
            Node startNode = new Node(mapaDeCostos.getCostoPosicion(start), start);
            Node goalNode = new Node(mapaDeCostos.getCostoPosicion(goal), goal);

            openStruct.Add(startNode);
            while(openStruct.Size > 0) {
                Node actualNode = openStruct.getMinimoAndRemove();
                //openList.Remove(actualNode);
                closeStruct.Add(actualNode);
                
                if(actualNode.Equals(goalNode)) {
                    return this.makeTheWay(startCheckpoint, goalCheckpoint, actualNode);
                }

                List<Node> adjacentNodes = actualNode.getAdjacent(mapaDeCostos, closeStruct);
                foreach(Node adjacent in adjacentNodes) {
                    if (!openStruct.Contains(adjacent))
                    {
                        // Entiendo que parent no como nodo padre 
                        // del arbol sino como nodo del que viene, cachai
                        //UF QUE MAL REDACTAS PAPA!!!! y si ya lo tenia entendido :P
                        adjacent.Parent = actualNode;
                        //NextNode se entiende como el nodo de donde venis quizas esta mal el nombre en el metodo.
                        adjacent.calculateCost(actualNode, goalNode.Point, mapaDeCostos);
                        // Lo muevo aca porque si se agrega antes de calcular los costos se agrega mal.
                        openStruct.Add(adjacent);
                    } else { 
                        Node nodo = openStruct.FindInStruct(adjacent);
                        if(nodo.GValue < adjacent.GValue) {
                            nodo.Parent = actualNode;
                            nodo.calculateCost(actualNode, goalNode.Point, mapaDeCostos);
                            //NO HACE FALTA ORDENAR... SE SAKA Y SE PONE ORDENADO
                            if (!openStruct.Remove(nodo))
                            {
                                Logger.appendWarning("El nodo no fue removido con exito. " + nodo.Point);
                            }
                            openStruct.Add(nodo);
                        }
                    }
                }
            }
            
            return null;
        }

        /**
         * Arma el camino encontrado recoriendo los padres, apartir del nodo final.
         */
        private Conexion makeTheWay(CheckPoint startCheckpoint, CheckPoint goalCheckpoint, Node goalNode)
        {
            //Hago la conexion alrevez ya que construimos el camino de final hacia principio.
            Conexion connection = new Conexion(goalCheckpoint, startCheckpoint);
            Node node = goalNode;
            while (node != null)
            {
                connection.AgregarPuntoCamino(node.Point);
                node = node.Parent;
            }
            return connection;
        }
    }
}