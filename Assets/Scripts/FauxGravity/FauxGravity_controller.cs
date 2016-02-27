using UnityEngine;
using System.Collections;

public class FauxGravity_controller : MonoBehaviour {

	public float moveSpeed = 15f;
	private Vector3 moveDirection;

	private Rigidbody body;

	// Use this for initialization
	void Start () {
	
		body = GetComponent <Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		// get direction of movement from input
		moveDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
	
	
	}


	// rigid body so used fixed update
	void FixedUpdate(){


		// moves body in local space (transform converts the global direction into local)
		body.MovePosition (body.position + transform.TransformDirection( moveDirection) * moveSpeed * Time.deltaTime);


	}
}
