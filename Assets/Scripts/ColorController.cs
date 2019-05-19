using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
	public Material[] materials;
	Renderer rend;

    void Start()
    {
		if (materials.Length < 3)
			Debug.LogError("Not enought materials in color controller");
		rend = GetComponent<Renderer>();
		rend.material = materials[0];
	}
	
	public void ChangeColor(int colorIndex)
	{
		rend.material = materials[colorIndex];
	}
}
