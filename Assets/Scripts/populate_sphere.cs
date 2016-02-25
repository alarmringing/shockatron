using UnityEngine;
using System.Collections;

public class populate_sphere : MonoBehaviour {

	public GameObject building;
	public GameObject planet;

	public void Start () {

		int building_number = 1000;

		for (int i = 0; i < building_number; i++) {
			
			// determine rotation and position of sphere
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + building.transform.localScale.y * 0.5f) + planet.transform.position;
			Quaternion spawnRotation = Quaternion.identity;

			// initatiate the object
			GameObject newBuiding = Instantiate(building, spawnPosition, spawnRotation) as GameObject;

			// transform the object
			newBuiding.transform.LookAt(planet.transform);
			newBuiding.transform.Rotate(-90, 0, 0);
		}


	}
		
}
