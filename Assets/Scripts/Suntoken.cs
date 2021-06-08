using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suntoken : Token
{
    private GameObject _gameObject;
    public Suntoken()
    {
        this._gameObject = Resources.Load("Prefabs/Suntoken") as GameObject;
    }
    public override void spawn(int pX, int pY)
    {
        GameObject block = Instantiate(this._gameObject, Vector3.zero, this._gameObject.transform.rotation) as GameObject;
        block.transform.localPosition = new Vector3(pX, 0.6f, pY);
        block.name = "sun_" + pX + "-" + pY;

        GameObject parent = GameObject.Find("Tokens"); 
        block.transform.parent = parent.transform;

    }
}
