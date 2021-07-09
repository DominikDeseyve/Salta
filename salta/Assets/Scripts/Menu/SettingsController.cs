using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private static SettingsController instance;
    public static SettingsController Instance {get { return instance; } }

    private Database database;
    public Settings settings;
    private void Awake() {
        instance = this;

        this.database = new Database();
        this.settings = this.database.loadSettings();    
    }

    public void saveSettings() {
        this.database.writeSettings(this.settings);
    }   
}
