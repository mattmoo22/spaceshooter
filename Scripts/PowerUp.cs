using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	private int chance;
	private PlayerController playerscript;
	public GUIText poweruptext;
	public GameController playerobject;
	public GameObject powerupparent;
	public int lasertime;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			playerobject = gameControllerObject.GetComponent <GameController>();
		}
		playerscript = playerobject.GetComponent <PlayerController>();
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.angularVelocity = Random.insideUnitSphere * 5;
		rb.velocity = transform.forward * -4;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Player")
		{
			chance = Random.Range(1,1);
			if (chance == 1)
			{
				playerobject.PowerupTime();
			}
			if (chance == 2)
			{

			}
			transform.position = new Vector3 (100,100,100);
		}
	}
}
