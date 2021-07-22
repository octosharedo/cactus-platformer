using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour {
	private const int amountLevels = 3;
	private int lvl;
	private readonly int[] levelLocations = new int[] { 6, 9, 12 };

	private bool isRecordDeaths, isRecordTime, isNotRecordDeath, isNotRecordTime;
	private float RecordTime;
	private int RecordDeaths; //what is set in PlayerPrefs after the script's work
	private bool isEverRecorded;

	private bool isHardMode;

	void Start ()
    {
		isRecordDeaths = isRecordTime = isNotRecordDeath = isNotRecordTime = false;
		isHardMode = GetComponent<HeroScript>().isHardMode;

		for (int i = 0; i < amountLevels; i++)
		{
			if (Application.loadedLevel == levelLocations[i])
			{
				lvl = i;
			}
		}   //number of current level
		isEverRecorded = PlayerPrefs.HasKey("RecordTime" + lvl.ToString() + isHardMode.ToString()); //check if current level was ever completed
		if (isEverRecorded)
		{
			RecordTime = PlayerPrefs.GetFloat("RecordTime" + lvl.ToString() + isHardMode.ToString());
			RecordDeaths = PlayerPrefs.GetInt("RecordDeaths" + lvl.ToString() + isHardMode.ToString());
		}   //getting previous records for time and deaths
	}   //setting information

    /// <summary>
	/// Returns the string which will be put on GUI's window after the level is complete;
	/// </summary>
	public string GetResultString(int deaths, float time)
	{
        string result = (isHardMode)
			? "\n[HARDMODE][RESULT]\nYou died " + deaths.ToString() + " times\nYour time is " + time.ToString() + " seconds\n\n"
			: "\n  [RESULT]\nYou died " + deaths.ToString() + " times\nYour time is " + time.ToString() + " seconds\n\n";

        if (isEverRecorded)
        {
            //check for records and save
            if (Compare(time))
                SaveRecord(time);
            if (Compare(deaths))
                SaveRecord(deaths);

            //editing resulting string
            RecordTime = PlayerPrefs.GetFloat("RecordTime" + lvl.ToString() + isHardMode.ToString());
            RecordDeaths = PlayerPrefs.GetInt("RecordDeaths" + lvl.ToString() + isHardMode.ToString());
            result += "Your deaths record is " + RecordDeaths.ToString();
            result += "\nYour time record is " + RecordTime.ToString();
        }  //compare with previous records, save and change the result string
        else
        {
            SaveRecord(time);
            SaveRecord(deaths);
        }  //just save received values and don't change the result string

        return result;
    }

    /// <summary>
    /// Returns True if the new record must be set
    /// </summary>
    private bool Compare(float time)
    {
        return (time < RecordTime);
    }
    private bool Compare(int deaths)
    {
        return (deaths < RecordDeaths);
    }
    private void SaveRecord(float time)
    {
        PlayerPrefs.SetFloat("RecordTime" + lvl.ToString() + isHardMode.ToString(), time);
    }
    private void SaveRecord(int deaths)
    {
        PlayerPrefs.SetInt("RecordDeaths" + lvl.ToString() + isHardMode.ToString(), deaths);
    }
}
