using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	private PlayerController playerscript;
	private int chance;
	public int hazardCount;
	public int lasertime;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float minsize;
	public float maxsize;
	public GUIText poweruptext;
	public GUIText scoretext;
	public GUIText roundtext;
	public GUIText gameOverText;
	public GUIText restartText;
	private float randomscale;
	private int roundnum;
	private int score;
	private bool debounce;
	private bool gameOver;
	private bool restart;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("Player");
		playerscript = gameControllerObject.GetComponent <PlayerController>();
		gameOver = false;
		restart = false;
		debounce = true;
		poweruptext.text = "";
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		roundnum = 1;
		roundtext.text = "";
		scoretext.text = "" + score;
		StartCoroutine (SpawnWaves ());
	}
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	IEnumerator SpawnWaves ()
	{
		for (int i = 0; i < (startWait/0.5); i++)
		{
			roundtext.text = "ROUND " + roundnum;
			yield return new WaitForSeconds (0.25f);
			roundtext.text = "";
			yield return new WaitForSeconds (0.25f);
		}
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				randomscale = Random.Range(minsize,maxsize);
				hazard.transform.localScale = new Vector3(randomscale,randomscale,randomscale);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
				if (gameOver)
				{
					restartText.text = "[R] Restart";
					restart = true;
					break;
				}
			}
			if (gameOver)
			{
				restartText.text = "[R] Restart";
				restart = true;
				break;
			}
			yield return new WaitForSeconds (7.5f);
			roundnum += 1;
			hazardCount += 25;
			for (int i = 0; i < (waveWait/0.5); i++)
			{
				if (gameOver)
				{
					restartText.text = "[R] Restart";
					restart = true;
					break;
				}
				roundtext.text = "ROUND " + roundnum;
				yield return new WaitForSeconds (0.25f);
				roundtext.text = "";
				yield return new WaitForSeconds (0.25f);
			}
		}
	} 
	public void AddScore (int newScore)
	{
		score += newScore;
		scoretext.text = "" + score;
	}
	public void GameOver ()
	{
		gameOverText.text = "G A M E   O V E R";
		gameOver = true;
	}
	public void PowerupTime ()
	{
		StartCoroutine (Timer());
	}
	IEnumerator Timer ()
	{
		if (debounce)
		{
			debounce = false;
			playerscript.LaserPowerup (0);
			for (int i = lasertime; i > 0; i--)
			{
				poweruptext.text = "!!! LAZER FOR " + i + " SECS !!!";
				yield return new WaitForSeconds(1);
			}
			playerscript.LaserPowerup(0.5f);
			poweruptext.text = "";
			debounce = true;
		}
	}
}
