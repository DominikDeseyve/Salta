using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode (int pX, int pY) {
        this.x = pX;
        this.y = pY;
        this.isWalkable = true;
    }

    public void CalculateFCost() {
        this.fCost = gCost + hCost;
    }

    public override string ToString() {        
       return x + "/" + y;        
    }
}
