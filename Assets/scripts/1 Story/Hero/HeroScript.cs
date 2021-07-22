using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HeroScript : MonoBehaviour {

	#region variables
	private const float speed = 4f;

    private int lvl; private const int amountLevels = 3;
    private readonly int[] levelLocations = new int[] { 6, 9, 12 };

    public int score = 0;
	const int maxJumps = 1;  public int jumpCount = 0;
	public int deaths; 
	public bool inGame; //False if the level is complete;

	bool isLoad;
	public bool isHardMode;
	public float plusTime; //time from loaded game

	public bool isEverRecorded;

	float spawnX, spawnY;  //start location for cactus

	GameObject triggerFamily; //NAMED "triggers"
    GameObject allPlatforms; //NAMED "platforms"

	Rigidbody2D rig;
    CoinManager coinMan;
	HardModeActivator hardmode;
    #endregion

    void Start () {
		rig = GetComponent<Rigidbody2D> ();
        coinMan = GetComponent<CoinManager>();
		hardmode = GetComponent<HardModeActivator>();

        allPlatforms = GameObject.Find("platforms");
        triggerFamily = GameObject.Find("triggers");

		spawnX = gameObject.transform.position.x; spawnY = gameObject.transform.position.y;
		deaths = 0;
		for (int i = 0; i < amountLevels; i++)
		{
			if (Application.loadedLevel == levelLocations[i])
			{
				lvl = i;
			}
		}   //number of current level
		isEverRecorded = PlayerPrefs.HasKey("RecordTime" + lvl.ToString() + isHardMode.ToString()); //check if current level was ever completed

		isLoad = (PlayerPrefs.HasKey("StartMode")) ? (PlayerPrefs.GetInt("StartMode") == 1) : false;
		isHardMode = (PlayerPrefs.HasKey("hardmode")) ? (PlayerPrefs.GetInt("hardmode") == 1) : false;
		inGame = true;
		RestartLevel();
		if (isLoad)
		{
			Load();
		}
		else
		{
			plusTime = 0f;
		}
	}

    void Update () {
		float move;
		if (inGame)
		{
			//moving left and right
			move = (hardmode.isFlipped) ? -Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal");
			rig.velocity = new Vector2(move * speed, rig.velocity.y);

			//jumping
			if (Input.GetButtonDown("Jump") && (jumpCount > 0))
			{
				rig.AddForce(new Vector2(0, 400f));
				jumpCount--;
			}

			//i want to die
			if (Input.GetKeyDown(KeyCode.Space))
			{
				deaths++;
				RestartLevel();
			}

			//save and go to menu
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				PlayerPrefs.SetInt("Load", Application.loadedLevel);
				Save();
				Application.LoadLevel(0);
			}

			//cheat
			if (Input.GetKeyDown(KeyCode.J))
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		} //controls
		if (!inGame && isEverRecorded)
		{
			if (Input.GetKeyDown(KeyCode.Y))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			if (Input.GetKeyDown(KeyCode.N))
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		} //ask if Player wants to restart lvl
		if (!inGame && !isEverRecorded)
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		} //load next scene
	}

	void OnTriggerEnter2D(Collider2D Col)
	{
		if (Col.GetComponent<CircleCollider2D>().tag=="coin") 
		{
            coinMan.CollectCoin(Col.gameObject.transform);
		}
	}

	void OnCollisionEnter2D(Collision2D Col) {
		//when Player touches the ground, he can jump again. Prevents possibility of infinite jumping in the air
		if (Col.gameObject.tag == "ground")
		{
			jumpCount = maxJumps;
		}

		//when Player is on moving platform, this platform is moving a Player too
		if (Col.gameObject.tag == "moving ground")
		{
			jumpCount = maxJumps;
			gameObject.transform.parent = Col.gameObject.transform;
		}
		if (Col.gameObject.tag != "moving ground")
		{
			jumpCount = maxJumps; //magic
			gameObject.transform.parent = null;
		}

        if (Col.gameObject.tag == "lever")
		{
			Col.gameObject.GetComponent<openingBridges>().Switch();
		}

		//spawns Player and coins back after death
		if (Col.gameObject.tag == "death") 
		{
			gameObject.transform.parent = null;
			jumpCount = 0;
			deaths++;
			RestartLevel();
		}	
	}

	void RestartLevel()
	{
		gameObject.transform.parent = null;
		gameObject.transform.position = new Vector2 (spawnX, spawnY);

        coinMan.SpawnCoins();
		score = 0;

		if (triggerFamily != null)
		{
			int triggersAmount = triggerFamily.transform.childCount;
			for (int i = 0; i < triggersAmount; i++)
			{
				triggerFamily.transform.GetChild(i).GetChild(0).GetComponent<openingBridges>().Open();
			}
		}

        if ( !isHardMode && (deaths == 100) && (allPlatforms != null))
            allPlatforms.GetComponent<helpMeImNoob>().Activate();

		if (hardmode.isFlipped) hardmode.Flip();
    }

	void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/loadInfo.dat");

		LoadData data = new LoadData();
		data.isHardMode = isHardMode;
		data.isFlipped = hardmode.isFlipped;
		data.deaths = deaths;
		data.time = Time.timeSinceLevelLoad + plusTime;
		data.score = score;
		data.xPosition = gameObject.transform.position.x; data.yPosition = gameObject.transform.position.y;

		Transform coins = GameObject.Find("coinFamily").transform;
		int amountCoins = coins.childCount;
		data.xCoins = new float[amountCoins];
		data.yCoins = new float[amountCoins];
		for (int i = 0; i < amountCoins; i++)
		{
			Vector2 pos = coins.GetChild(i).position;
			data.xCoins[i] = pos.x;
			data.yCoins[i] = pos.y;
		}

		bf.Serialize(file, data);
		file.Close();
	}

	void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/loadInfo.dat"))
		{
			//take the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/loadInfo.dat", FileMode.Open);
			LoadData data = (LoadData) bf.Deserialize(file);
			file.Close();

			//put the data from file into game
			isHardMode = data.isHardMode;
			if (data.isFlipped) hardmode.Flip();
			deaths = data.deaths;
			plusTime = data.time;
			score = data.score;
			gameObject.transform.position = new Vector2(data.xPosition, data.yPosition);

			Transform coins = GameObject.Find("coinFamily").transform;
			int amountCoins = coins.childCount;
			for (int i = 0; i < amountCoins; i++)
			{
				coins.GetChild(i).position = new Vector2(data.xCoins[i], data.yCoins[i]);
			}
		}
		PlayerPrefs.SetInt("StartMode", 0);
	}
}

/// <summary>
/// data for save and load
/// </summary>
[Serializable]
class LoadData
{
	public bool isHardMode;
	public bool isFlipped;
	public int deaths;
	public float time;
	public int score;
	public float xPosition, yPosition;
	public float[] xCoins;
	public float[] yCoins;
}