using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Wait some time and go to the next scene.
/// </summary>
public class waiting : MonoBehaviour {
	int waitingTime;
	const int LastStoryScene = 16;
	const int DoorScenePlusOne = 14;

	public void Start()
	{
		if (Application.loadedLevel >= DoorScenePlusOne)
			waitingTime = 1;
		else
			waitingTime = 3;
	}
	public void Update()
	{
		//здесь код, который должен выполняться ДО ожидания
		if (Application.loadedLevel == LastStoryScene)
		{
			SetWallpaper.change();
			Application.LoadLevel(0); //back to main menu
		}
		else
		{
			StartCoroutine(Wait(waitingTime));
		}
		//здесь код, который должен выполняться ВО ВРЕМЯ ожидания
	}
	public IEnumerator Wait(int time)
	{
		yield return new WaitForSeconds(time);// Ждем time секунд.
		//здесь код, который выполняется ПОСЛЕ ожидания
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
