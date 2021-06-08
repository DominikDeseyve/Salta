using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startoken : Token
{
    private GameObject _gameObject;
    public Startoken()
    {
        this._gameObject = Resources.Load("Prefabs/Startoken") as GameObject;
    }
  
    public override void spawn(int pX, int pY)
    {
        GameObject block = Instantiate(this._gameObject, Vector3.zero, this._gameObject.transform.rotation) as GameObject;
        block.transform.localPosition = new Vector3(pX, 0.6f, pY);
        block.name = "star_" + pX + "-" + pY;

        GameObject parent = GameObject.Find("Tokens"); 
        block.transform.parent = parent.transform;
    }


}
