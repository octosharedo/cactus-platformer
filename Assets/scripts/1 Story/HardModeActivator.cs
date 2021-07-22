using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardModeActivator : MonoBehaviour {

	GameObject mcamera, background;
	private bool isHardMode;
	public bool isFlipped;

	void Start () {
		isFlipped = false;
		mcamera = GameObject.Find("Main Camera");
		background = GameObject.Find("location");
	}

	public void Flip()
	{
		if (GetComponent<HeroScript>().isHardMode)
		{
			Vector3 position = gameObject.transform.position;
			mcamera.transform.Rotate(0, 0, 180);
			background.transform.Rotate(0, 0, 180);
			gameObject.transform.position = position;
			isFlipped = !isFlipped;
		}
	}
}
