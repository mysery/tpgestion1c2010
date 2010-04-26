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
        private BinaryTree<Node> openList;
        private BinaryTree<Node> closeList;
        
        public AStar()
        {
            openList = new BinaryTree<Node>();
            closeList = new BinaryTree<Node>();
        }

        /**
         * Busca un camino entre startCheckpoint y goalCheckpoint teniendo en cuenta los costos del mapaDeCostos.
         * Implementando el algoritmo A*
         */
        public Conexion pathFind(CheckPoint startCheckpoint, CheckPoint goalCheckpoint, MapaDeCostos mapaDeCostos)
        {
            // TODO ???? TODO QUE NEWBIE????
            Point start = startCheckpoint.Posicion;
            Point goal = goalCheckpoint.Posicion;
            Node startNode = new Node(mapaDeCostos.getCostoPosicion(start), start);
            Node goalNode = new Node(mapaDeCostos.getCostoPosicion(goal), goal);

            openList.Add(startNode);
            while(openList.Size > 0) {
                Node actualNode = openList.getMinimoAndRemove();
                //openList.Remove(actualNode);
                closeList.Add(actualNode);
                
                if(actualNode.Equals(goalNode)) {
                    return this.makeTheWay(startCheckpoint, goalCheckpoint, actualNode);
                }

                List<Node> adjacentNodes = actualNode.getAdjacent(mapaDeCostos, closeList);
                foreach(Node adjacent in adjacentNodes) {
                    if (!openList.Contains(adjacent))
                    {
                        // Entiendo que parent no como nodo padre 
                        // del arbol sino como nodo del que viene, cachai
                        //UF QUE MAL REDACTAS PAPA!!!! y si ya lo tenia entendido :P
                        adjacent.Parent = actualNode;
                        //NextNode se entiende como el nodo de donde venis quizas esta mal el nombre en el metodo.
                        adjacent.calculateCost(actualNode, goalNode.Point, mapaDeCostos);
                        // Lo muevo aca porque si se agrega antes de calcular los costos se agrega mal.
                        openList.Add(adjacent);
                    } else { 
                        Node nodo = openList.Find(adjacent).Value;
                        if(nodo.GValue < adjacent.GValue) {
                            nodo.Parent = actualNode;
                            nodo.calculateCost(actualNode, goalNode.Point, mapaDeCostos);
                            //NO HACE FALTA ORDENAR... SE SAKA Y SE PONE ORDENADO
                            if (!openList.Remove(nodo))
                            {
                                Logger.appendWarning("El nodo no fue removido con exito. " + nodo.Point);
                            }
                            openList.Add(nodo);
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
