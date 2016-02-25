using UnityEngine;
using System.Collections;

public class populate_sphere : MonoBehaviour {

	public GameObject building;
	public GameObject planet;

	public void Start () {

		int building_number = 500;

		for (int i = 0; i < building_number; i++) {
			
			// determine rotation and position of sphere
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + building.transform.localScale.y * 0.3f) + planet.transform.position;
			Quaternion spawnRotation = Quaternion.identity;

			// initatiate the object
			GameObject newBuiding = Instantiate(building, spawnPosition, spawnRotation) as GameObject;

			// transform the object
			newBuiding.transform.LookAt(planet.transform);
			newBuiding.transform.Rotate(-90, 0, 0);


			// randomly scale the size of the object
			float width_scale =  Random.Range(-2F, 2F);
			float height_scale = Random.Range (-2F, 2F);
			newBuiding.transform.localScale += new Vector3(width_scale,height_scale  ,width_scale);
			//newBuiding.transform.position -= new Vector3 (0, height_scale+1, 0);
		}


	}
		
}
