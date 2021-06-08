using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSelectorCreator : MonoBehaviour
{
    [SerializeField] private Material freeFieldMaterial;
    [SerializeField] private Material enemyFieldMaterial;

    [SerializeField] private GameObject selectorPrefab;

    private List<GameObject> instantiatedSelectors = new List<GameObject>();


    public void showSelection(List<Vector3> pFieldData) {
        this.clearSelectors();
        foreach (var data in pFieldData) 
        {
            GameObject selector = Instantiate(this.selectorPrefab, data, Quaternion.identity);
            instantiatedSelectors.Add(selector);
            foreach (var setter in selector.GetComponentsInChildren<MaterialSetter>())
            {
                setter.SetSingleMaterial(freeFieldMaterial);
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
