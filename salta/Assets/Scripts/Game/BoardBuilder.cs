using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [Header("Art Stuff")]
    [SerializeField] private Material tileMaterial1;
    [SerializeField] private Material tileMaterial2;
    [SerializeField] private int tileCountX = 10;
    [SerializeField] private int tileCountY = 10;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private Color highlightedColor = Color.red;
    private Vector2Int highlightedField;
    
    private GameObject[,] tiles;
    private Camera currentCamera;
    private Vector2Int currentHover;
    private bool tileIsColored = false;
    private bool newRow = false;


    private void Awake()
    {
        generateAllTiles(tileSize, tileCountX, tileCountY);
    }
   

    private void generateAllTiles(float size, int tileCountX, int tileCountY)
    {
        tiles = new GameObject[tileCountX, tileCountY];

        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                tiles[x, y] = generateSingleTile(size, x, y);
            }

            newRow = !newRow;
            tileIsColored = false;
        }
    }

    private GameObject generateSingleTile(float size, int x, int y)
    {
        GameObject tileObject = new GameObject(string.Format($"X:{x}, Y:{y}"));
        tileObject.transform.parent = transform;
        tileObject.transform.localPosition = Vector3.zero;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;

        if (newRow)
        {
            if (tileIsColored)
                tileObject.AddComponent<MeshRenderer>().material = tileMaterial1;
            else 
                tileObject.AddComponent<MeshRenderer>().material = tileMaterial2;

            tileIsColored = !tileIsColored;
        }
        else 
        {
            if (tileIsColored)
                tileObject.AddComponent<MeshRenderer>().material = tileMaterial2;
            else 
                tileObject.AddComponent<MeshRenderer>().material = tileMaterial1;

            tileIsColored = !tileIsColored;
        }

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * size, 0, y * size);
        vertices[1] = new Vector3(x * size, 0, (y + 1) * size);
        vertices[2] = new Vector3((x + 1) * size, 0, y * size);
        vertices[3] = new Vector3((x + 1) * size, 0, (y + 1) * size);

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        tileObject.layer = LayerMask.NameToLayer("Tile");
        tileObject.AddComponent<BoxCollider>();

        return tileObject;
    }

    public void selectField(Vector2Int pCoords) {
        this.highlightedField = pCoords;      
        this.tiles[pCoords.x, pCoords.y].GetComponent<MeshRenderer>().material.color = this.highlightedColor;
    }
    public void deselectField() {
        this.tiles[this.highlightedField.x, this.highlightedField.y].GetComponent<MeshRenderer>().material = tileMaterial1;        
    }   
}
