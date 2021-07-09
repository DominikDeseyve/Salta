using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Overlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  playerText;

    [SerializeField] private TextMeshProUGUI  countText;

    [SerializeField] private TextMeshProUGUI jumpBox;

    public void displayPlayer(Player pPlayer) {       
        this.playerText.SetText(pPlayer.name.ToString() + " ist dran!");
    }

    public void displayCount(int pCount) {
        this.countText.SetText("Anzahl Züge: " + pCount);
    }

    public void showSalta(Player pActivePlayer) {
        this.jumpBox.enabled = true;
        this.jumpBox.color = pActivePlayer.color;
        StartCoroutine(fadeInSalta(this.jumpBox, 3));
    }
    

    IEnumerator fadeInSalta(TextMeshProUGUI pGameObject, float pSeconds)
     { 
         yield return new WaitForSeconds(pSeconds); 
         this.jumpBox.enabled = false;    
     }
}
