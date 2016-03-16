using UnityEngine;
using System.Collections;



/* This class attracts a rigid body to 
 * the surface of a sphere
 * 
 */ 

public class FauxGravityAttracter : MonoBehaviour {

	public float gravity = -50;
	public float rotation_speed = 50f;


	public void Attract(Transform body) {

		// wanteed body to be facing towards gravity up
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up; /// direction body currently facing

		Rigidbody body_rb = body.GetComponent<Rigidbody> ();
		body_rb.AddForce(gravityUp * gravity);


		// deal with rotation (add current rotation to diff in rotations
		Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

		// get body to move towards this target rotation smoothly
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotation_speed*Time.deltaTime);

	}
		
}
