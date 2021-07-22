using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCactus : MonoBehaviour {

	Transform obj; //moving object
	Vector3 posA, posB; 
	bool endOfMove; 

	public  float speed;

	void Start () {
		obj = gameObject.transform.GetChild(0);
		posA = gameObject.transform.GetChild(1).localPosition;
		posB = gameObject.transform.GetChild(2).localPosition;
		if (speed == 0)
			speed = 0.7f;
	}

	void Update()  
	{	
		Vector3 currentPos = obj.transform.localPosition;
		endOfMove = currentPos == posB;
		if (!endOfMove)
			obj.transform.localPosition = Vector3.MoveTowards(currentPos, posB, Time.deltaTime * speed);
		else
			Application.LoadLevel(Application.loadedLevel + 1);
	}

}
