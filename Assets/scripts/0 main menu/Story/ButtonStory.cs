using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStory : MonoBehaviour {

	const float currentVersion = 1.3f;

	private void Start()
	{
		if (!PlayerPrefs.HasKey("version") || (PlayerPrefs.GetFloat("version") != currentVersion))
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetFloat("version", currentVersion);
		}
	}

	void OnMouseDown()
	{
		Application.LoadLevel(2);
	}
}
