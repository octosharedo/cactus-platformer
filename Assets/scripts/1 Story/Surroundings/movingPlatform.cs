using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the object family consisting of
/// Parent: Empty
/// Child 1: Moving object
/// Child 2: First position, empty object
/// Child 3: Second position, empty object
/// </summary>

public class movingPlatform : MonoBehaviour {

	Transform obj; //moving object
	Vector3 posA, posB;
	bool endOfMove; //true - obj has reached posB

	public const float speed = .5f;

	void Start () {
		obj = gameObject.transform.GetChild(0);
		posA = gameObject.transform.GetChild(1).localPosition;
		posB = gameObject.transform.GetChild(2).localPosition;
	}
	
	void Update()  
	{	
		Vector3 currentPos = obj.transform.localPosition;
		if (endOfMove = currentPos == posB)
		{
			Vector3 tmp = posA;
			posA = posB;
			posB = tmp;
		}
		obj.transform.localPosition = Vector3.MoveTowards(currentPos, posB, Time.deltaTime * speed);
	}
}
