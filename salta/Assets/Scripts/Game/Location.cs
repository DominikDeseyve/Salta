using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Location 
{
    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public Location Parent; 

    public static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
}
