using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStoryHardmode : MonoBehaviour {

	public Sprite off, on;

	void Start () {
		if (!PlayerPrefs.HasKey("hardmode"))
		{
			PlayerPrefs.SetInt("hardmode", 0);
		}

		gameObject.GetComponent<SpriteRenderer>().sprite = (PlayerPrefs.GetInt("hardmode") == 1) ? on : off;
	}

	private void OnMouseDown()
	{
		if (PlayerPrefs.GetInt("hardmode") == 1)
		{
			PlayerPrefs.SetInt("hardmode", 0);
			gameObject.GetComponent<SpriteRenderer>().sprite = off;
		}
		else
		{
			PlayerPrefs.SetInt("hardmode", 1);
			gameObject.GetComponent<SpriteRenderer>().sprite = on;
		}
	}
}
