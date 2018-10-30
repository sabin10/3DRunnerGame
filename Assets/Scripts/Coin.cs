using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {


	public float speed = 20f;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.forward, speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			GameManager.collected = true;

		}
	}

}