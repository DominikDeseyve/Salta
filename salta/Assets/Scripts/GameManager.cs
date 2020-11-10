using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public GameObject cell; 
     public GameObject token; 
 
     public int worldWidth  = 10;
     public int worldHeight  = 10;
 
     void  Start () {        
        CreateWorld();
        CreateToken();
    }
 
     void CreateWorld () {
        Debug.Log("Create World");
        Color white = new Color(255, 255, 255);
        Color black = new Color(0, 0, 0);

         for(int x = 0; x < worldWidth; x++) { 
             for(int z = 0; z < worldHeight; z++) { 
                GameObject block = Instantiate(cell, Vector3.zero, cell.transform.rotation) as GameObject;
                block.transform.parent = transform;
                if (x % 2 == z % 2) {
                    block.GetComponent<Renderer>().material.color = black;
                } else {
                    block.GetComponent<Renderer>().material.color = white;
                }                
                block.transform.localPosition = new Vector3(x, 0, z);
             }
         }
     }    

     void CreateToken() {
        Debug.Log("Create Token");

        for(int x = 0; x < worldWidth; x++) { 
             for(int z = 0; z < worldHeight; z++) {
                if (x % 2 == z % 2)
                {
                    GameObject block = Instantiate(token, Vector3.zero, token.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, 0.6f, z);
                }
            }
         }

     }
     

    // Update is called once per frame
    void Update()
    {
        
    }
}
