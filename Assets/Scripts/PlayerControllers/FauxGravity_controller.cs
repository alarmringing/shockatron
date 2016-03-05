using UnityEngine;
using System.Collections;



/* 
 * This class controls the movement of an
 * object attracted to a sphere with faux gravity
 * 
 */ 

public class FauxGravity_controller : MonoBehaviour {

	// set rigid body
	private Rigidbody body;

	// set speed controls
	public float testSpeed = 1f;
	public float moveSpeed = 50f;
	private float jumpSpeed = 10f;
	private Vector3 moveDirection;


	// Use this for initialization
	void Start () {
		body = GetComponent <Rigidbody> ();
	}


	// Update is called once per frame
	void Update () {

		// get direction of movement from input
		moveDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 1).normalized;
	
	}


	// rigid body so used fixed update
	void FixedUpdate(){

		// moves body in local space (transform converts the global direction into local)
		body.MovePosition (body.position + transform.TransformDirection( moveDirection) * moveSpeed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Space)){
			Vector3 jump = new Vector3 (0, jumpSpeed, 0);
			body.velocity += transform.TransformDirection (jump);
		}


	}
}
