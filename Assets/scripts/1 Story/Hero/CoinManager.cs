using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    #region variables

    GameObject coinFamily;
    Vector2[] allCoinsPositions;

    int maxScore;

    HeroScript hero;
    GuiScript ui;
    RecordManager records;
	HardModeActivator hardmode;

    #endregion

    void Start ()
    {
        hero = gameObject.GetComponent<HeroScript>();
        ui = gameObject.GetComponent<GuiScript>();
        records = gameObject.GetComponent<RecordManager>();
		hardmode = GetComponent<HardModeActivator>();

        coinFamily = GameObject.Find("coinFamily");
        maxScore = coinFamily.transform.childCount;
        allCoinsPositions = new Vector2[maxScore];
        for (int i = 0; i < maxScore; i++)
        {
            allCoinsPositions[i] = coinFamily.transform.GetChild(i).position;
        }
	}

    public void CollectCoin(Transform coin)
    {
        coin.position = new Vector2(-5, -5); //hide the coin
        hero.score++;
		hardmode.Flip();
        if (hero.score == maxScore)
        {
            float finalTime = Time.timeSinceLevelLoad; finalTime += hero.plusTime;
            hero.inGame = false;
            int deaths = hero.deaths;
            if (records != null) ui.result = records.GetResultString(deaths, finalTime);
        }
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < maxScore; i++)
        {
            coinFamily.transform.GetChild(i).position = allCoinsPositions[i];
        }
    }
}
