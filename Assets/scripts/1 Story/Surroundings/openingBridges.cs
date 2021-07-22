using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// trigger-family:
/// 	Parent: Empty
/// 	Child 1: trigger (button, lever, etc)
/// 	Child 2-N: bridges
/// 
/// GAMEOBJECT - TRIGGER (child 1)
/// BigFamily of all trigger-families must exist, this family must be put on cactus' heroScript's place for this family. otherwise it will not work.
/// 
/// USING:
/// trigger-family is a prefab, it has 1 bridge. Just copy this bridge as many times as you want*, and this trigger will open all of its brother-bridges.
/// Don't forget to create an empty BigFamily and put all trigger-families there.
/// 
/// * maximum amount of bridges for one trigger is a constant (check heroScript)
/// </summary>
public class openingBridges : MonoBehaviour {

	bool isOpened;

	int angle = 90; //degrees

	const int MaxAmountOfBridges = 5;
	public int amountBridges;
	Transform construction;
	Transform[] bridges = new Transform[MaxAmountOfBridges]; //for one lever

	public Sprite leverOFF;
	public Sprite leverON;


	void Start () {
		isOpened = false;
		construction = gameObject.transform.parent;
		amountBridges = construction.childCount - 1; //because one of them is a lever, not a bridge
		for (int i = 0; i < amountBridges; i++)
		{
			bridges[i] = construction.GetChild(i+1);
		}
		Open();
	}

	public void Open()
	{
		if (!isOpened)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = leverOFF;
			for (int i = 0; i < amountBridges; i++)
			{
				bridges[i].GetChild(0).Rotate(new Vector3(0, 0, angle));
				bridges[i].GetChild(1).Rotate(new Vector3(0, 0, -angle));
			}
			angle = -angle;
			isOpened = true; 
		}
	}

	public void Close()
	{
		if (isOpened)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = leverON;
			for (int i = 0; i < amountBridges; i++)
			{
				bridges[i].GetChild(0).Rotate(new Vector3(0, 0, angle));
				bridges[i].GetChild(1).Rotate(new Vector3(0, 0, -angle));
			}
			angle = -angle;
			isOpened = false;
		}
	}

	public void Switch()
	{
		if (isOpened) Close();
		else Open();
	}

}