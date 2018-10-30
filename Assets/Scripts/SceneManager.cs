using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

	private PlayerMovement motor;

	private void Start()
	{
		motor = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}

	public void RestartLevel()
	{
		Application.LoadLevel (Application.loadedLevel);
		motor.StopRunnig ();
		Time.timeScale = 1;
	}
}
