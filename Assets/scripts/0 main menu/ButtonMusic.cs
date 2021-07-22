using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusic : MonoBehaviour
{
	public Sprite musicOff;
	public Sprite musicOn;

	void Start()
	{
		if (!PlayerPrefs.HasKey("music"))
		{
			PlayerPrefs.SetInt("music", 1);
		}

		gameObject.GetComponent<SpriteRenderer>().sprite = (PlayerPrefs.GetInt("music") == 1) ? musicOn : musicOff;
	}

	void OnMouseDown()
	{
		if (PlayerPrefs.GetInt("music") == 1)
		{
			PlayerPrefs.SetInt("music", 0);
			gameObject.GetComponent<SpriteRenderer>().sprite = musicOff;
		}
		else
		{
			PlayerPrefs.SetInt("music", 1);
			gameObject.GetComponent<SpriteRenderer>().sprite = musicOn;
		}
	}

}
