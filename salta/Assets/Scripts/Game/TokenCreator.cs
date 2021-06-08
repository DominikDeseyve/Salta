using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] sunTokenPrefab;
    [SerializeField] private GameObject[] starTokenPrefab;

    [SerializeField] private GameObject[] moonTokenPreb;

    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;
    private Dictionary<TokenType, GameObject[]> tokenToPrefab = new Dictionary<TokenType, GameObject[]>();
    
    private void Awake() {        
        tokenToPrefab.Add(TokenType.Sun, sunTokenPrefab);
        tokenToPrefab.Add(TokenType.Moon, moonTokenPreb);
        tokenToPrefab.Add(TokenType.Star, starTokenPrefab);        
    }

    public GameObject createToken(TokenType pType, Vector2Int pCoords, int pValency) {
        
        
        GameObject prefab = this.tokenToPrefab[pType][pValency-1];
        Debug.Log("valency: " + pValency.ToString());
        if(prefab) {
            GameObject newToken = Instantiate(prefab);            
            return newToken;
        }
        return null;
    }

   public Material getPlayerMaterial(TeamType pPlayer)
    {
        return pPlayer == TeamType.Player1 ? whiteMaterial : blackMaterial;
    }
}
