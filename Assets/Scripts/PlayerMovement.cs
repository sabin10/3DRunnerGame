 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private const float distanceLane = 2.0f;
	private CharacterController Player;
	private float jumpforce = 4.5f;
	private float gravity  = 9.81f;
	private float verticalVelocity;
	private float speed = 5.0f;
	private int desiredLane = 1;

	private float startSpeed = 7.0f;
	private float speedIncreaseLastTick;
	private float speedIncreaseTime= 2.5f;
	private float speedIncreaseAmount = 0.1f;
	Vector3 movement;

	public static bool isRunning = false;
	public static bool isCounting = true;
	private GameObject DeathUI;


	private Animator anim;

	// Use this for initialization
	private void Start () {

		speed = startSpeed;
		Player = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		DeathUI = GameObject.FindGameObjectWithTag ("DeathUI");
		DeathUI.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isRunning)
		{
			
			if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
			{
				speedIncreaseLastTick = Time.time;
				speed += speedIncreaseAmount;

			}
				

			if (Input.GetKeyDown (KeyCode.A))
				MoveLane (false);
			if (Input.GetKeyDown (KeyCode.D))
				MoveLane (true);

			Vector3 targetPosition = transform.position.z * Vector3.forward;
			if (desiredLane == 0)
				targetPosition += Vector3.left * distanceLane;
			else if (desiredLane == 2)
				targetPosition += Vector3.right * distanceLane;

			Vector3 moveVector = Vector3.zero;
			moveVector.x = (targetPosition - transform.position).normalized.x * speed;

			bool isGrounded = IsGrounded ();
			anim.SetBool ("Grounded", isGrounded);

			if (isGrounded) {
			
				verticalVelocity = -0.1f;



				if (Input.GetKeyDown (KeyCode.Space)) {
					FindObjectOfType<AudioManager> ().Play ("Jump");
					anim.SetTrigger ("Jump");
					verticalVelocity = jumpforce;


				} else if (Input.GetKeyDown (KeyCode.S))
				{
					StartSliding ();
					Invoke ("StopSliding", 1.0f);
				}

			} else {
				verticalVelocity -= (gravity * Time.deltaTime);
			}

			moveVector.y = verticalVelocity;
			moveVector.z = speed;

			Player.Move (moveVector * Time.deltaTime);


		}
		if (isCounting == false) 
		{
			GameManager.isDead = true;
		}
	}

	private void MoveLane(bool goRight)
	{
		if (!goRight) {
			desiredLane--;
			if (desiredLane == -1)
				desiredLane = 0;
		}
		else
		{
			desiredLane++;
			if (desiredLane == 3)
				desiredLane = 2;
		}
	}

	private bool IsGrounded(){

		Ray groundRay = new Ray (new Vector3 (Player.bounds.center.x, (Player.bounds.center.y - Player.bounds.extents.y) + 0.2f, Player.bounds.center.z), Vector3.down);

		return Physics.Raycast (groundRay, 0.2f + 0.1f);
	}

	public void StartRunning()
	{
		isRunning = true;
		anim.SetTrigger ("StartRunning");
	}

	public void StopRunnig()
	{
		isRunning = false;
	}

	private void StartSliding()
	{
		anim.SetBool ("Sliding", true);
		Player.height /= 4;
		Player.center = new Vector3 (Player.center.x, Player.center.y / 4, Player.center.z);
	}

	private void StopSliding()
	{
		anim.SetBool ("Sliding", false);
		Player.height *= 4;
		Player.center = new Vector3 (Player.center.x, Player.center.y * 4, Player.center.z);
	}

	private void Collided()
	{
		anim.SetTrigger ("Death");
		isRunning = false;
		isCounting = false;
		DeathUI.SetActive (true);

	
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		switch (hit.gameObject.tag) {
		case "Obstacle":
			Collided ();
			break;
		}
	}
}
