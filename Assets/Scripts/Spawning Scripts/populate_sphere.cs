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
	public GameObject musicController_delay;

	public int object_number = 200;
	private int number_freq = 1024;
	public float maxWidthScale; 
	public float maxHeightScale;
	Vector3[] ObjOriginPos;

	public bool setOutline = false;
	//public Color defaultColor;
	public int numberColors = 5;
	private float colorAdjust = 255f;

	//public Color color1;
	//public Color color2;
	//public Color color3;
	//public Color color4;
	//public Color color5;
	public Vector4 defaultColor = new Vector4 (0, 0, 0, 255);
	public Vector4 color1  = new Vector4(122f, 255f, 0f, 255f);
	public Vector4 color2  = new Vector4(31f, 196f, 244f, 255f);
	public Vector4 color3  = new Vector4(0f, 61f, 244f, 255f);
	public Vector4 color4  = new Vector4(45f, 44f, 155f, 255f);
	public Vector4 color5  = new Vector4(0f, 116f, 188f, 255f);

	//NOTE was trying to have the buildings have different colorings depending on frequency levels. Isn't working now though
	Vector4 beginColor = new Vector4(0f, 0f, 255f, 255f);
	Vector4 endColor = new Vector4(0,255f,0,255f);


	// at run time
	public void Start () {
		
		//set this Array
		ObjOriginPos = new Vector3[object_number];

		// sound is not synchronized for the objects and some still arent moving

		// set default colors
		int color = 0; 
		Color[]  colors = new Color[5];
		colors [0] = color1/colorAdjust;
		colors[1] =  color2/colorAdjust;
		colors[2] =  color3/colorAdjust;
		colors[3] =  color4/colorAdjust;
		colors[4] =  color5/colorAdjust;


		// create object_number of objects
		for (int i = 0; i < object_number; i++) {

			/* CREATE NEW GAME OBJECT ON SURFACE OF SPHERE */

			// determine rotation and position of sphere
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + surface_object.transform.localScale.y * 0.5f) + planet.transform.position;


			// initatiate the object
			GameObject newObject = Instantiate(surface_object, spawnPosition, spawnRotation) as GameObject;
		
			// transform the object
			newObject.transform.LookAt(planet.transform);
			newObject.transform.Rotate(90, 0, 0);

			/* RANDOMIZE THE WIDTH OF THE OBJECT */

			// randomly scale the size of the object
			float width_scale =  Random.Range(0, maxWidthScale);
			float height_scale = Random.Range (0F, maxHeightScale);
			newObject.transform.localScale += new Vector3(width_scale, height_scale, width_scale);

			// Move object towards sphere
			Vector3 sphereDirection =  (newObject.transform.position - planet.transform.position).normalized;
			newObject.transform.position -= sphereDirection * newObject.transform.localScale.y/2;

			//Save into array to Pass origin position to buildinganimations
			ObjOriginPos[i] = newObject.transform.position;

			// adjust the bounding box
			//BoxCollider buildingCollider = newObject.GetComponent<BoxCollider>();
			//Vector3 renderSize = newObject.GetComponent<Renderer>().bounds.size;
			//buildingCollider.size = new Vector3(1,1,1);
			//buildingCollider.size = new Vector3(renderSize.x, renderSize.y, renderSize.z);


			/* SET THE COLOR */ 
			Renderer objectRender = newObject.GetComponent<Renderer> ();
			Vector4 outlineColor = defaultColor;
			//Vector4 fillColor = colors [color];
			Vector4 fillColor = Color.Lerp(beginColor,endColor,((float)i)/((float)object_number));

			if (setOutline) {
				outlineColor = fillColor;
				fillColor = defaultColor;
			} 
			objectRender.material.SetColor("_OutlineColor",outlineColor);
			objectRender.material.color = fillColor;


			color++;
			if (color == numberColors -1) color = 0;
		}

		//if finished spawning
		musicController_delay.GetComponent<BuildingAnimations>().OnSpawnFinished(ObjOriginPos);


		//BroadcastMessage ("OnSpawnFinished");

	}
		

		
}
