using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource moveSound;    

    [SerializeField] private AudioSource victorySound;

    [SerializeField] private AudioSource saltaSound;

    [SerializeField] private AudioSource jumpSound;
    
    public void playMove() {
        if(this.isSoundActivated()) {
            moveSound.Play();
        }        
    }    
    public void playJump() {
        if (this.isSoundActivated()) {
            jumpSound.Play();
        }
    }

    public void playSalta() {
        if (this.isSoundActivated()) {
            saltaSound.Play();
        }
    }
    public void playVictory() {
        if (this.isSoundActivated()) {
            victorySound.Play();
        }
    }

    private bool isSoundActivated() {
        return SettingsController.Instance.settings.enableSound;
    }
}
