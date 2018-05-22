using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astedroidmover : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject asteroidparent;
	public GameObject powerUp;
	private GameObject clone;
	private GameObject powerScript;
	public int scoreValue;
	public int poweruprate;
	private GameController gameController;
	private float sizespeed;
	private int random;
	public int hitpoints;
	public float speed;
	public float tumble;

	void Start () //Rotates randomly
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (asteroidparent.transform.localScale.x < 2.5)
		{
			sizespeed = Random.Range(-1.0f,-1.5f);
			tumble = 1;
			hitpoints = 3;
			scoreValue = 500;
		}
		if (asteroidparent.transform.localScale.x < 2)
		{
			sizespeed = Random.Range(-2.0f,-2.5f);
			tumble = 2;
			hitpoints = 2;
			scoreValue = 300;
		}
		if (asteroidparent.transform.localScale.x < 1.5)
		{
			sizespeed = Random.Range(-3.0f,-3.5f);
			tumble = 4;
			hitpoints = 1;
			scoreValue = 200;
		}
		if (asteroidparent.transform.localScale.x < 1)
		{
			sizespeed = Random.Range(-4.0f,-4.5f);
			tumble = 6;
			hitpoints = 1;
			scoreValue = 200;
		}
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		rb.velocity = transform.forward * sizespeed * speed;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary")
		{
			return;
		}
		if (other.tag == "Player") //Delete when hit player
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		if (other.tag == "Asteroid")
		{
			return;
		}
		Destroy(other.gameObject); //Destroys Bullet
		hitpoints += (-1);
		if (hitpoints == 0)
		{
			gameController.AddScore (scoreValue);
			Instantiate(explosion, transform.position, transform.rotation); //Explosion
			random = Random.Range (1,poweruprate);
			if (random == 2)
			{
				clone = Instantiate (powerUp, transform.position,Quaternion.identity);
			}
			Destroy(asteroidparent); //Destroy asteroid
		}
	}

	void OnTriggerExit(Collider boundary) { //Delete when exiting boundary
		if (boundary.tag == "Boundary")
		{
			Destroy(asteroidparent);
		}
	}
}
