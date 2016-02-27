using UnityEngine;
using System.Collections;

public class BuildingSpawner : MonoBehaviour {
	public float spawnTime = 3f;		// The amount of time between each spawn.
	public float spawnDelay = 0f;		// The amount of time before spawning starts.

	public float spawnCrowTime = 8f;
	public float spawnCrowDelay = 6f;

	private GameObject [] object_prefabs;		// Array of prefabs.
	private int number;

	// public game object for all of the prefabs
	//public GameObject[] all_prefabs;	


	// Initializaes the spawning script
	void Start () {
		// creates new game object and loads the prefab to be that game object
		object_prefabs = new GameObject[1];
		object_prefabs [0] = Resources.Load<GameObject> ("Prefabs/Bird");
		number = 0;

		// invokes "Spawn" method, starting at time spawnDelay and then repeats at spawnTime intervals
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	// The spawning method
	void Spawn ()
	{
		// set the max number particles and return if theyve been reached
		int max_particles = 15;
		if(number>=max_particles)return;

		// the position of the object initializes at 0,0,0
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);

		// Create 5 (4 created) objects per spawn
		for(int i=0; i<5; i++){
			// define the range of the random numbers
			float r = 10.0f;
			// determine a random perturbation for each object to initialize each in different spots
			float x_perturb = Random.Range (-r, r);
			float y_perturb = Random.Range (-r, r);
			float z_perturb = Random.Range (-r, r);
			// create a position vector for each particle based on the above perturbations
			Vector3 pos = new Vector3(transform.position.x+x_perturb, transform.position.y+y_perturb, transform.position.z+z_perturb);
			// create the prefab object at "pos" with rotation transform.rotation (The rotation of the transform in world space stored as a Quaternion.)
			Instantiate(object_prefabs[0], pos, transform.rotation);
			number++;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}