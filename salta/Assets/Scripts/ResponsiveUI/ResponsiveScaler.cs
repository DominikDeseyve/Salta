using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveScaler : MonoBehaviour
{
    [Header("-- Overlay --")]
    [SerializeField] float percentageofScreenHeight;

    private Vector2 resolution;    
    void Awake()
    {        
        this.resolution = new Vector2(Screen.width, Screen.height);
        this.resize();
    }
    void Update()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height) {
            this.resize();
            this.resolution = new Vector2(Screen.width, Screen.height);
        }
    }

    void resize() {
        //Overlay height
        float y = ((100-percentageofScreenHeight) / 100) * Screen.height;
        RectTransform rt = GetComponent<RectTransform>();        
        rt.offsetMin = new Vector2(rt.offsetMin.x, y);
    }

    
}
