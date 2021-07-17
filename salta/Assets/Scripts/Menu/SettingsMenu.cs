using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle enableFieldSelector;
    [SerializeField] private Toggle enableSelection;

    [SerializeField] private Toggle enableSound;

    [SerializeField] private Dropdown viewport;
    void Start() {        
        enableFieldSelector.isOn = SettingsController.Instance.settings.enableFieldSelector;
        enableSelection.isOn = SettingsController.Instance.settings.enableSelection;
        enableSound.isOn = SettingsController.Instance.settings.enableSound;
        viewport.value = SettingsController.Instance.settings.view;
        
    }
    public void navigateBack() {      
        SceneManager.LoadScene("menu");
    }

    public void setEnableHints(bool pIsEnabled) {
        SettingsController.Instance.settings.enableFieldSelector = pIsEnabled;
        SettingsController.Instance.saveSettings();
    }
    public void setEnableSelection(bool pIsEnabled) {
        SettingsController.Instance.settings.enableSelection = pIsEnabled;
        SettingsController.Instance.saveSettings();
    }

    public void setViewport() {
        SettingsController.Instance.settings.view = this.viewport.value;
        SettingsController.Instance.saveSettings();     
    }

     public void setEnableSound(bool pIsEnabled) {
        SettingsController.Instance.settings.enableSound = pIsEnabled;
        SettingsController.Instance.saveSettings();     
    }
}
