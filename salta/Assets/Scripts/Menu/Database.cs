using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database
{
    private string preffix = "SALTA_";      

    public Settings loadSettings() {        
        Settings settings = this.getDefaultSettings();

        if(PlayerPrefs.HasKey(preffix + "enable-fieldselector")) {
            settings.enableFieldSelector = this.convertIntToBool(PlayerPrefs.GetInt(preffix + "enable-fieldselector"));
        }
        if(PlayerPrefs.HasKey(preffix + "enable-selection")) {
            settings.enableSelection = this.convertIntToBool(PlayerPrefs.GetInt(preffix + "enable-selection"));
        }
        if(PlayerPrefs.HasKey(preffix + "enable-sound")) {
            settings.enableSound = this.convertIntToBool(PlayerPrefs.GetInt(preffix + "enable-sound"));
        }
        if(PlayerPrefs.HasKey(preffix + "view")) {
            settings.view = PlayerPrefs.GetInt(preffix + "view");
        }
        return settings;
    }
    public void writeSettings(Settings pSettings) {
        PlayerPrefs.SetInt(preffix + "enable-fieldselector", convertBoolToInt(pSettings.enableFieldSelector));
        PlayerPrefs.SetInt(preffix + "enable-selection", convertBoolToInt(pSettings.enableSelection));
        PlayerPrefs.SetInt(preffix + "enable-sound", convertBoolToInt(pSettings.enableSound));
        PlayerPrefs.SetInt(preffix + "view", pSettings.view);
    }

    private Settings getDefaultSettings() {
        Settings settings = new Settings();
        settings.enableFieldSelector = true;
        settings.enableSelection = true;
        settings.enableSound = false;
        settings.view = 0;
        return settings;
    }


    /* HELPER */
    private int convertBoolToInt(bool pBool) {
        return (pBool ? 1 : 0);
    }
    private bool convertIntToBool(int pInt) {
          return (pInt == 1 ? true : false);
    }
}
