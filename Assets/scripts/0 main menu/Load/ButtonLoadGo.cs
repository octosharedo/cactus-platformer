using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadGo : MonoBehaviour {

	public GameObject musicBoxPrefab; 
	GameObject musicBox;

	void Start () {
		if (!PlayerPrefs.HasKey("Load"))
		{
			gameObject.SetActive(false);
		}
	}

	void OnMouseDown()
	{
		musicBox = Instantiate(musicBoxPrefab) as GameObject; DontDestroyOnLoad(musicBox);
		PlayerPrefs.SetInt("StartMode", 1);
		Application.LoadLevel(PlayerPrefs.GetInt("Load"));
	}
}
