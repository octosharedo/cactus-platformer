using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel(0);
	}
}
