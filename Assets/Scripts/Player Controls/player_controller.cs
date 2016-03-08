using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour {

	// set up animator control
	private Rigidbody rigid;

	// set up paramaters for actions
	public float speed = 0.0f;
	public float maxSpeed = 300.0f;
	public float turnSpeed = 100.0f;
	public float acceleration = 200.0f;
	public float decelleration = 400.0f;


	// Use this for initialization
	void Start () {
		// get the animation for the object
		rigid = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		// Get input for walking
		float forward = Input.GetAxis ("Vertical");
		float turn = Input.GetAxis("Horizontal");

		// handle speed
		speed += acceleration * Time.deltaTime * forward;
		if (Mathf.Abs (forward) < Mathf.Epsilon) {
			speed -= decelleration*Time.deltaTime;
		}
		speed = Mathf.Clamp (speed, 0, maxSpeed);


		// handle movement
		transform.Translate (0, 0, speed/acceleration/5);
		transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);

		// if player hits escape, move to game end scene
		if (Input.GetKeyDown(KeyCode.Escape) ){
			Debug.Log("Pressed escape");
			Application.LoadLevel ("GameEndScene");
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			Debug.Log("Requesting Pauce");
			Application.LoadLevel ("GameEndScene");
		}
	}
		
		

}
