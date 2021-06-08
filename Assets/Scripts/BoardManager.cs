using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int worldWidth = 10;
    public int worldHeight = 10;

    public Field[,] _gamefield = new Field[10, 10];

    void Start()
    {
        _spawnGame();

    }
    void Update() {
        Debug.Log("test");

    }
    private void _spawnGame()
    {
        Debug.Log("Create World");

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                //Spawne das Schachbrett
                this._gamefield[x, z] = new Field(x, z);
                this._gamefield[x, z].spawnGround();

                //Ordne die richtigen Tokens zu
                if (z == 0 || z == worldHeight - 1)
                {
                    this._gamefield[x, z].token = new Startoken();
                }
                else if (z == 1 || z == worldHeight - 2)
                {
                    this._gamefield[x, z].token = new Moontoken();
                }
                else if (z == 2 || z == worldHeight - 3)
                {
                    this._gamefield[x, z].token = new Suntoken();
                }
                else
                {
                    this._gamefield[x, z].token = new Emptytoken();
                }
                //TODO: Wertigkeit setzen

                //Spawne Sternsteine auf der ersten Zeile des Spielfelds 
                this._gamefield[x, z].spawn();
            }
        }
    }


}
