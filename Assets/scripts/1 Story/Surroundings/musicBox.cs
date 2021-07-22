using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicBox : MonoBehaviour {

	public int birthLevel;
	bool isCongratulation;
	const int DoorScene = 13;
	const int DoorScenePlusOne = DoorScene + 1;

	void Start () {
		if (PlayerPrefs.GetInt("music") == 0)
			Destroy(gameObject);
		isCongratulation = Application.loadedLevel == DoorScene;
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		if ((Application.loadedLevel == DoorScene)&&(!isCongratulation)) 
		{
			Destroy(gameObject);
		} //destroy old music, start happy birthday
		if (Application.loadedLevel == DoorScenePlusOne)
		{
			Destroy(gameObject);
		}
		if (Application.loadedLevel == 0)
		{
			Destroy(gameObject);
		}
	}
}
