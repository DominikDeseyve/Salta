using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialSetter : MonoBehaviour
{
	private MeshRenderer _meshRenderer;
	private MeshRenderer meshRenderer
	{
		get
		{
			if (_meshRenderer == null)
				_meshRenderer = GetComponent<MeshRenderer>();
			return _meshRenderer;
		}
	}

	public void SetSingleMaterial(Material material)
	{		
		Material[] mats = meshRenderer.materials;   
		if(mats.Length > 1) {
			
 			mats[0] = material;
			mats[2] = material;
			meshRenderer.materials = mats;
		} else {
			meshRenderer.material = material;
		}
        
    }
}