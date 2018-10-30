using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public static bool gameIsPaused = false;
	public static bool canPause = false;

	public GameObject pauseMenuUI;

	void Start()
	{
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update () {

		if (canPause) 
		{
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				if (gameIsPaused) 
				{
					Resume ();
				} 
				else
				{
					Pause ();
				}
			}
		}

	}

	public void Resume()
	{
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	void Pause()
	{
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}
}
