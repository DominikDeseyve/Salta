using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database
{
    private string preffix = "SALTA_";
    
    public Database() {

    }

    public Settings loadSettings() {
        Settings settings = new Settings();
        if(PlayerPrefs.HasKey(preffix + "enable-fieldselector")) {
            settings.enableFieldSelector = this.convertIntToBool(PlayerPrefs.GetInt(preffix + "enable-fieldselector"));
        }
        return settings;
    }
    public void writeSettings(Settings pSettings) {
        PlayerPrefs.SetInt(preffix + "enable-fieldselector", convertBoolToInt(pSettings.enableFieldSelector));
    }
    /* HELPER */
    private int convertBoolToInt(bool pBool) {
        return (pBool ? 1 : 0);
    }
    private bool convertIntToBool(int pInt) {
          return (pInt == 1 ? true : false);
    }
}
