using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiScript : MonoBehaviour {

    public string result;
	HeroScript hero;

    void Start ()
    {
        hero = gameObject.GetComponent<HeroScript>();
	}

    void OnGUI()
    {
        string timePast = string.Format("{0:0.00}", Time.timeSinceLevelLoad + hero.plusTime);

        if (hero.inGame)
        {
            GUI.Box(new Rect(0, 0, 90, 50),
                "Score = " + hero.score.ToString()
                + "\nDeaths = " + hero.deaths.ToString()
                + "\nTime = " + timePast);
        }
        if (!hero.inGame && hero.isEverRecorded)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 3),
                result + "\n\nDo you want to restart this level?\nPress Y or N");
        }
    }
}
