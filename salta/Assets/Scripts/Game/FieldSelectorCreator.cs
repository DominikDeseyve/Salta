using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSelectorCreator : MonoBehaviour
{
    [SerializeField] private Material greenFieldMaterial;
    [SerializeField] private Material redFieldMaterial;

    [SerializeField] private GameObject selectorPrefab;

    private List<GameObject> instantiatedSelectors = new List<GameObject>();


    public void showSelection(Dictionary<Vector3, Color> pFieldData) {
        this.clearSelectors();
        Debug.Log(pFieldData.Count);
        if(!SettingsController.Instance.settings.enableFieldSelector) return;
        
        foreach (var data in pFieldData) {          
            GameObject selector = Instantiate(this.selectorPrefab, data.Key, Quaternion.identity);           
            instantiatedSelectors.Add(selector);           
            foreach (var setter in selector.GetComponentsInChildren<MaterialSetter>()) {
                if(data.Value == Color.red) {
                    setter.SetSingleMaterial(redFieldMaterial);
                } else {
                    setter.SetSingleMaterial(greenFieldMaterial);
                }               
            }
        }
    }

    public void clearSelectors() {
        foreach (var selector in this.instantiatedSelectors)
        {
            Destroy(selector.gameObject);
        }
    }

}
