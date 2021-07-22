using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStoryGo : MonoBehaviour {

	const int StoryScene = 5;

	public GameObject musicBoxPrefab;
	GameObject musicBox;

	void OnMouseDown()
	{
		musicBox = Instantiate(musicBoxPrefab) as GameObject;
		DontDestroyOnLoad(musicBox);
		PlayerPrefs.SetInt("StartMode", 0);
		Application.LoadLevel(StoryScene);
	}
}
