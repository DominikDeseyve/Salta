using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveScaler : MonoBehaviour
{
    [Header("-- Overlay --")]
    [SerializeField] float percentageofScreenHeight;

    void Awake()
    {
        //Overlay height
        float y = ((100-percentageofScreenHeight) / 100) * Screen.height;
        RectTransform rt = GetComponent<RectTransform>();        
        rt.offsetMin = new Vector2(rt.offsetMin.x, y);
    }

    
}
