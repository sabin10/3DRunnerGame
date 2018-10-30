using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



	public static bool isDead = false;
	public static bool collected = false;
	private bool isGameStarted = false;
	private PlayerMovement motor;
	public Text scoreText, coinText;
	private float score, coinCollect, scoreIncrease = 1f;
	public GameObject startGame;
	public GameObject quitGame;

	private const int coinScoreAmount = 5;

	// Use this for initialization
	void Awake () {


		motor = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		coinText.text ="Coins: " + coinCollect.ToString ("0");
		scoreText.text = "Total Score: " + score.ToString ("0");
		startGame.SetActive (true);
		quitGame.SetActive (true);
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && !isGameStarted)
		{
			isGameStarted = true;
			motor.StartRunning ();
			StartCoroutine ("ScoreModifier");
			PauseGame.canPause = true;
			startGame.SetActive (false);
			quitGame.SetActive (false);
		}

		if (isGameStarted && !isDead) 
		{
			score += Time.deltaTime * scoreIncrease;
			scoreText.text = "Total Score: " + score.ToString ("0");
		}

		if (collected) {
			
			CollectCoin ();
			collected = false;
		}

	}



	IEnumerator ScoreModifier(){
		while (true) {
			yield return new WaitForSeconds (2f);
			scoreIncrease += 0.1f;
		}
	}

	private void CollectCoin()
	{
		coinCollect ++;
		coinText.text ="Coins: " + coinCollect.ToString ("0");
		score += coinScoreAmount;
		scoreText.text = "Total Score: " + score.ToString ("0");

	}

	public void ButtonStart()
	{
		if (!isGameStarted) 
		{
			isGameStarted = true;
			motor.StartRunning ();
			StartCoroutine ("ScoreModifier");
			startGame.SetActive (false);
			PauseGame.canPause = true;
		}
	}
}
