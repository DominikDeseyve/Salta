using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private Database database;
    private Settings settings;

    void Start()
    {
        
    }
    void Awake() {
        this.database = new Database();
        this.settings = this.database.loadSettings();
        Debug.Log(this.settings.enableFieldSelector);
    }
    void Update()
    {
        
    }
}
