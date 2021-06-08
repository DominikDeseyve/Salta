using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Field(int pX, int pY)
    {
        this.currentX = pX;
        this.currentY = pY;

    }
    public Token token
    {
        set;
        get;
    }

    public int currentX
    {
        set;
        get;
    }

    public int currentY
    {
        set;
        get;
    }

    public void spawn()
    {
        if (this.isBlack())
        {
            this.token.spawn(this.currentX, this.currentY);
        }
    }

    public void spawnGround()
    {
        GameObject cube = Resources.Load("Prefabs/Cube") as GameObject;

        GameObject block = Instantiate(cube, Vector3.zero, cube.transform.rotation) as GameObject;
        block.transform.localPosition = new Vector3(this.currentX, 0, this.currentY);
        block.name = "field_" + currentX + "-" + currentY;

        GameObject parent = GameObject.Find("Board"); 
        block.transform.parent = parent.transform;

        if (this.isBlack())
        {
            block.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
        else
        {
            block.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }
    }

    public bool isBlack()
    {
        return (this.currentX % 2 == this.currentY % 2);
    }

}
