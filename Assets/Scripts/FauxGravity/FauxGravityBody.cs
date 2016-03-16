using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttracter attractor;
	private Transform myTransform;
	private Rigidbody body;

	// Use this for initialization
	void Start () {
		// set rotation and gravity to be fixed
		body = GetComponent <Rigidbody> ();
		body.constraints = RigidbodyConstraints.FreezeRotation;
		body.useGravity = false;

		myTransform = transform;
	}

	// Update is called once per frame
	void FixedUpdate () {

		attractor.Attract(myTransform);


	}

}
