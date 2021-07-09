using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame() {       
        SceneManager.LoadScene("game");
    }

    public void openSettings() {       
        SceneManager.LoadScene("settingsMenu");
    }
    public void exitGame() {       
        Application.Quit();
    }
}
