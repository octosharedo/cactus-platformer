using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// platforms become wider
/// </summary>

public class helpMeImNoob : MonoBehaviour {

	int amountPlatforms; 
	public bool isActive;

	void Start () {
		amountPlatforms = gameObject.transform.childCount;
		isActive = false;
	}

	public void Activate()
	{
		for (int i = 0; i < amountPlatforms; i++)
			{
				Vector3 tmp = gameObject.transform.GetChild(i).localScale;
				tmp.x *= 1.5f;
				gameObject.transform.GetChild(i).localScale = tmp;
			}
		isActive = true;
	}

	public void Disactivate()
	{
		for (int i = 0; i < amountPlatforms; i++)
			{
				Vector3 tmp = gameObject.transform.GetChild(i).localScale;
				tmp.x /= 1.5f;
				gameObject.transform.GetChild(i).localScale = tmp;
			}
		isActive = false;
	}
}
