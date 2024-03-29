﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/* 
 * This class controls the movement of an
 * object attracted to a sphere with faux gravity
 * 
 */ 

public class FauxGravity_controller : MonoBehaviour {

	// set rigid body
	private Rigidbody body;

	// speed up audio
	AudioSource speedUp;

	// set speed controls
	//public float inputMoveSpeed = 0.8f;
	public float moveSpeed = 80f;
	public float mouseMultiplier = 1f;
	private Vector3 moveDirection;
	private int inverted;



	// Use this for initialization
	void Start () {
		body = GetComponent <Rigidbody> ();
		inverted = PlayerPrefs.GetInt ("inversion");
		AudioSource[] audioSources = GetComponents<AudioSource>();
		speedUp = audioSources[0];
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKey("space"))
		{
			moveSpeed = 150f; //when attacking mode, faster
			if(!speedUp.isPlaying) speedUp.Play(); //play attack mode boost music
		}
		else 
		{
			moveSpeed = 100f; 
			speedUp.Stop(); //stop attack mode boost music
		}

		//float horizontalInput = mouseMultiplier*Input.GetAxis ("Mouse X") + Input.GetAxisRaw ("Horizontal");
		//float verticalInput = inverted*(mouseMultiplier*Input.GetAxis ("Mouse Y") + Input.GetAxisRaw ("Vertical"));
		//sorry, disabilg mouse for now feels awkward
		float horizontalInput = inverted*Input.GetAxisRaw ("Horizontal");
		float verticalInput = Input.GetAxisRaw ("Vertical");

		// get direction of movement from input
		moveDirection = new Vector3 (horizontalInput, verticalInput, 1f).normalized;

		// if player hits escape, move to game end scene
		if (Input.GetKeyDown(KeyCode.Escape) ){
			Debug.Log("Pressed escape");
			SceneManager.LoadScene ("GameEndScene");
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			Debug.Log("Requesting Pause");
			SceneManager.LoadScene ("GameEndScene");
		}
	
	}


	// rigid body so used fixed update
	void FixedUpdate(){

		// moves body in local space (transform converts the global direction into local)
		body.MovePosition (body.position + transform.TransformDirection( moveDirection) * moveSpeed * Time.deltaTime);
		// transforms the rotation of the object to match the direction of movement
		GetComponentInChildren<MeshRenderer>().transform.localRotation = Quaternion.Lerp(GetComponentInChildren<MeshRenderer>().transform.localRotation, Quaternion.LookRotation(moveDirection), Time.deltaTime*10); //rotate towards movement


	}
}
