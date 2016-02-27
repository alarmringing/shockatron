using UnityEngine;
using System.Collections;


/* This script populates the surface of a sphere with the object
 * attached to the sphere. The number of objects on the planet can be defined.
 * The height and width of the object are randomly changed for each object.
 * The center of the object will be located on the surface of the sphere.
 */

public class populate_sphere : MonoBehaviour {

	public GameObject surface_object;
	public GameObject planet;

	public int object_number = 200;


	// at run time
	public void Start () {

		// create object_number of objects
		for (int i = 0; i < object_number; i++) {
			
			// determine rotation and position of sphere
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + surface_object.transform.localScale.y * 0.5f) + planet.transform.position;

			// initatiate the object
			GameObject newObject = Instantiate(surface_object, spawnPosition, spawnRotation) as GameObject;

			// transform the object
			newObject.transform.LookAt(planet.transform);
			newObject.transform.Rotate(-90, 0, 0);


			// randomly scale the size of the object
			float width_scale =  Random.Range(-10F, 10F);
			float height_scale = 0; //Random.Range (-1F, 1F);

			newObject.transform.localScale += new Vector3(width_scale, height_scale, width_scale);
			//newBuiding.transform.position -= new Vector3 (0, height_scale+1, 0);
		}


	}
		
}
