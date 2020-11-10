using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moontoken : Token
{
    private GameObject _gameObject;
    public Moontoken()
    {
        this._gameObject = Resources.Load("Prefabs/Moontoken") as GameObject;
    }
    public override void spawn(int pX, int pY)
    {
        GameObject block = Instantiate(this._gameObject, Vector3.zero, this._gameObject.transform.rotation) as GameObject;
        block.transform.localPosition = new Vector3(pX, 0.6f, pY);
        //block.transform.parent = _gameObject.transform;
    }
}
