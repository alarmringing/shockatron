using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttracter attractor;
	private Transform myTransform;
	private Rigidbody body;
	public GameObject planet;

	// Height limiting CONTROLS
	private float startingDistance;
	public float heightLimit = 2f;
	public float minHeight = 10f;
	private float maxHeight;


	// Use this for initialization
	void Start () {
		// set rotation and gravity to be fixed
		body = GetComponent <Rigidbody> ();
		body.constraints = RigidbodyConstraints.FreezeRotation;
		body.useGravity = false;

		myTransform = transform;
		// determine the starting position of the object
		maxHeight = heightLimit*(planet.transform.position - transform.position).sqrMagnitude;
	}

	// Update is called once per frame
	void FixedUpdate () {

		// If within the correct distance from the planet
		float currentDistance = (planet.transform.position - transform.position).sqrMagnitude;
		if ((currentDistance > minHeight) && (currentDistance < maxHeight)) {
			attractor.Attract (myTransform);
		}

	}

}
