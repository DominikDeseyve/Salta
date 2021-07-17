using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    private List<PathNode> openList;
    private List<PathNode> closeList;

    private const int MOVE_STRAIGH_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private int GRID_SIZE = 10;
    private PathNode[,] grid;
    public PathFinding (int pGridSize) {
        this.GRID_SIZE = pGridSize;
    }

    public void setGrid(PathNode[,] pGrid) {
        this.grid = pGrid;
    }

   public int findPath (int pStartX, int pStartY, int pEndX, int pEndY) {

        PathNode startNode = this.grid[pStartX, pStartY];
        PathNode endNode = this.grid[pEndX, pEndY];
        this.openList = new List<PathNode> { startNode };
        this.closeList = new List<PathNode>();

        //Init pathNodes
        for (int x = 0; x < GRID_SIZE; x++) {
             for (int y = 0; y < GRID_SIZE; y++) {
                PathNode pathNode = this.grid[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = this.CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();


        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode) {
                //reached final node
                return CalculateLength(endNode);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach(PathNode neightbourNode in this.getNeighbourList(currentNode)) {
                if(closeList.Contains(neightbourNode)) {
                    continue;
                }
                if(!neightbourNode.isWalkable) {
                    closeList.Add(neightbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neightbourNode);
                if(tentativeGCost < neightbourNode.gCost) {
                    neightbourNode.cameFromNode = currentNode;
                    neightbourNode.gCost = tentativeGCost;
                    neightbourNode.hCost = CalculateDistanceCost(neightbourNode, endNode);
                    neightbourNode.CalculateFCost();

                    if(!openList.Contains(neightbourNode)) {
                        openList.Add(neightbourNode);
                    }
                }
            }
        }

        //Out of nodes on the openList
        return -1;
    }
    private List<PathNode> getNeighbourList(PathNode pCurrentNode) {
        List<PathNode> neighbourList = new List<PathNode>();
        if(pCurrentNode.x - 1 >= 0) {
            //Left Down
            if(pCurrentNode.y - 1  >= 0) {
                neighbourList.Add(this.grid[pCurrentNode.x - 1, pCurrentNode.y - 1]);                
            }
            //Left Up
            if(pCurrentNode.y + 1 < this.GRID_SIZE) {
                neighbourList.Add(this.grid[pCurrentNode.x - 1, pCurrentNode.y + 1]);                
            }
        }

        if(pCurrentNode.x + 1 < this.GRID_SIZE) {
            //Right Down
            if(pCurrentNode.y - 1  >= 0) {
                neighbourList.Add(this.grid[pCurrentNode.x + 1, pCurrentNode.y - 1]);                
            }
            //Right Up
            if(pCurrentNode.y + 1 < this.GRID_SIZE) {
                neighbourList.Add(this.grid[pCurrentNode.x + 1, pCurrentNode.y + 1]);                
            }
        }
        return neighbourList;
    }
    private int CalculateLength(PathNode pEndNode) {
        int length = 0;
        PathNode currentNode = pEndNode;
        while(currentNode.cameFromNode != null) {
            length++;
            currentNode = currentNode.cameFromNode;
        }
        return length;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b) {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGH_COST * remaining;

    }

    private PathNode GetLowestFCostNode(List<PathNode> pNodeList) {
        PathNode lowestFCostNode = pNodeList[0];
        for (int i = 1; i < pNodeList.Count; i++) {
            if(pNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
