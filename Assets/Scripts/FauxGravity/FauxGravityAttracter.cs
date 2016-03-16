using UnityEngine;
using System.Collections;



/* This class attracts a rigid body to 
 * the surface of a sphere
 * 
 */ 

public class FauxGravityAttracter : MonoBehaviour {

	public float gravity = -8;
	public float rotation_speed = 50f;

	private bool found_start = false;
	private float startingDistance;
	public float heightLimit = 1.001f;


	public void Attract(Transform body) {

		// set starting distance
		if (found_start == false) {
			startingDistance = (body.position - transform.position).sqrMagnitude;
			found_start = true;
		}

		// wanteed body to be facing towards gravity up
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up; /// direction body currently facing

		Rigidbody body_rb = body.GetComponent<Rigidbody> ();



		// if get to far, apply additional force
		if ((body.position - transform.position).sqrMagnitude < startingDistance){
			//body_rb.AddForce(gravityUp * heightLimit);
			// make body head towards planet (add force in direction from center of planet to player
			body_rb.AddForce(gravityUp * gravity);
		}

		// deal with rotation (add current rotation to diff in rotations
		Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

		// get body to move towards this target rotation smoothly
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotation_speed*Time.deltaTime);

	}
		
}
