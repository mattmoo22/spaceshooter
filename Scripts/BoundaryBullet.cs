using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBullet : MonoBehaviour {
	public GameObject bullet;
	public int speed;
	void Start ()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}
	void OnTriggerExit(Collider boundary) { //Delete when exiting boundary
		if (boundary.tag == "Boundary")
		{
			Destroy(bullet);
		}
	}
}
