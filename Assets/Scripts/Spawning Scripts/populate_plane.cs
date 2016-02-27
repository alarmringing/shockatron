using UnityEngine;
using System.Collections;

public class populate_plane : MonoBehaviour {


	public GameObject surface_object;
	public GameObject plane;

	public int object_number = 200;
	public int placement_max = 300;


	// at run time
	public void Start () {

		// create object_number of objects
		for (int i = 0; i < object_number; i++) {

			float x_pos =  Random.Range(-placement_max, placement_max);
			float y_pos = Random.Range (-placement_max, placement_max);


			// determine rotation and position of sphere
			Vector3 spawnPosition = new Vector3(x_pos, 0, y_pos);

			// initatiate the object
			GameObject newObject = Instantiate(surface_object, spawnPosition, Quaternion.identity) as GameObject;

			// randomly scale the size of the object
			float width_scale =  Random.Range(-3F, 3F);
			float height_scale = Random.Range (-1F, 1F);

			newObject.transform.localScale += new Vector3(width_scale, height_scale, width_scale);
			//newBuiding.transform.position -= new Vector3 (0, height_scale+1, 0);

		}


	}
}
		
