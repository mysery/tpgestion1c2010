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

        public AStar() {
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
            while(openList.Count > 0) {
                Node actualNode = openList.getMinimo();
                closeList.Add(actualNode);
                
                if(actualNode.Equals(goalNode)) {
                    return this.makeTheWay(startCheckpoint, goalCheckpoint, actualNode);
                }

                List<Node> adjacentNodes = actualNode.getAdjacent(mapaDeCostos);
                foreach(Node adjacent in adjacentNodes) {
                    if (!openList.Contains(adjacent)) {
                        // Entiendo que parent no como nodo padre 
                        // del arbol sino como nodo del que viene, cachai
                        //UF QUE MAL REDACTAS PAPA!!!! y si ya lo tenia entendido :P
                        adjacent.Parent = actualNode;
                        //NextNode se entiende como el nodo de donde venis quizas esta mal el nombre en el metodo.
                        adjacent.calculateCost(actualNode.Point, goalNode.Point);
                        // Lo muevo aca porque si se agrega antes de calcular los costos se agrega mal.
                        openList.Add(adjacent);
                    } else { 
                        Node nodo = openList.Find(adjacent).Value;
                        if(nodo.GValue < adjacent.GValue) {
                            nodo.Parent = actualNode;
                            nodo.calculateCost(actualNode.Point, goalNode.Point);
                            // TODO Reordenar la lista Abierta.
                            //openList.REORDENAR
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
            Conexion connection = new Conexion(startCheckpoint, goalCheckpoint);
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
