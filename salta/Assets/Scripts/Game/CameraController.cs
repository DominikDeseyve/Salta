using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    
    void Start()
    {
        int view = SettingsController.Instance.settings.view;
        switch(view) { 
            case 0:
                this.transform.position = new Vector3(1361.487f,615.1838f,250.237f);
                this.transform.rotation = Quaternion.Euler(50.3f,89.92101f,-0.003f);
                break;
            case 1:
                this.transform.position = new Vector3(1372.983f,613.6459f,250.4709f);
                this.transform.rotation = Quaternion.Euler(85.46301f,89.994f,-0.017f);
                break;
            default:
                break;
        }
    }

    
}
