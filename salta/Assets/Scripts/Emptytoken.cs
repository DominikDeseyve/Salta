using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emptytoken : Token
{
    private GameObject _gameObject;
    public Emptytoken()
    {
        this._gameObject = Resources.Load("Prefabs/Cube") as GameObject;
    }
    public override void spawn(int pX, int pY)
    {

    }
}
