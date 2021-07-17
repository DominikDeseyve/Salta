using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject UIRestartScreen;
    [SerializeField] private Text resultText;

    [SerializeField] private Text remainingText;

    public void hideScreen() {
        this.UIRestartScreen.SetActive(false);
    }

    public void onGameFinished(Player pPlayer, int pRemainingMoves) {
        this.UIRestartScreen.SetActive(true);
        if(pRemainingMoves > 1) {
            this.remainingText.text = "mit " + pRemainingMoves + " Spielzügen";
        } else {
            this.remainingText.text = "mit einem einzelnen Spielzug";
        }
        
        this.resultText.text = "Gewinner: " + pPlayer.name;
    }
}
