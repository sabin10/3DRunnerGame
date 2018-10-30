using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPLayer : MonoBehaviour {

	private Transform playerTrans;

	// Use this for initialization
	void Start () {
		playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.forward * playerTrans.position.z;
	}
}
