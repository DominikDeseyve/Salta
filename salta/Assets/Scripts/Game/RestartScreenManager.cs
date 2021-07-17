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
        this.remainingText.text = "mit " + pRemainingMoves + " Spielzügen";
        this.resultText.text = "Gewinner: " + pPlayer.name;
    }
}
