using UnityEngine;
using System.Collections;

public class BuildingAnimations : MonoBehaviour {

	public GameObject planet;

	// controls for the buildings
	GameObject[] buildings;
	bool buildingSpawned = false;

	// audio paramters
	AudioSource thisAudio;
	float[] samples;
	Vector3[] ObjOriginPos;
	bool originPosSet = false;
	float stretchIntensity = 50f;
	float baseHeight = 25f;
	int numSamples = 1024;
	float maxStretch = 200f;

	// Use this for initialization
	void Start () {
		thisAudio = gameObject.GetComponent<AudioSource>();
		samples = new float[numSamples];

	}

	public void OnSpawnFinished(Vector3[] BuildingsPos)
	{		
		buildings = GameObject.FindGameObjectsWithTag ("building");
		/*
		for(int j=0; j<buildings.Length; j++) 
		{
			Debug.Log("J is " + j);
			Debug.Log("buildings is " +  buildings[j].transform);

			//Debug.Log("buildingSpawned is " + buildingSpawned + " and its pos is " + buildings[i].transform.position.ToString());
			//ObjOriginPos[j] = buildings[j].transform.position;
		}*/

		ObjOriginPos = BuildingsPos;

		buildingSpawned = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		thisAudio.GetSpectrumData(samples, 0, FFTWindow.Hamming);

		float minValue = float.MaxValue;
		for(int j=0; j<numSamples; j++)
		{
			if(samples[j] < minValue) minValue = samples[j];
		}

		if(buildingSpawned){
			for(int j=0; j<buildings.Length; j++) 
			{
				SetBoxScale(buildings[j], samples, j);
			}
		}
	
	}

	void SetBoxScale(GameObject Obj, float[] samples, int boxId) {

		float avg = samples[samples.Length/buildings.Length * boxId];
		float stretchValue =  Mathf.Clamp01(avg) * maxStretch;
		Vector3 previousScale = Obj.transform.localScale;

		previousScale.y = Mathf.Lerp(previousScale.y, baseHeight + stretchValue * stretchIntensity, Time.deltaTime * 10);
		Obj.transform.localScale = previousScale;

		// change the box collider to the same scale
		//BoxCollider buildingCollider = Obj.GetComponent<BoxCollider>();
		//Vector3 renderSize = Obj.GetComponent<Renderer>().bounds.size;
		//buildingCollider.size = new Vector3(1,1,1);
		//buildingCollider.size = renderSize;
		//buildingCollider.center = new Vector3 ((renderSize.x / 2),(renderSize.y / 2), (renderSize.z / 2));
		//buildingCollider.size = previousScale;


		/*
		Vector3 sphereDirection =  (Obj.transform.position - planet.transform.position).normalized;*/


		Vector3 sphereDirection =  (ObjOriginPos[boxId] - planet.transform.position).normalized;
		Obj.transform.position = ObjOriginPos[boxId] + sphereDirection * Obj.transform.localScale.y/2;
		//Obj.transform.position -= sphereDirection * Obj.transform.localScale.y;
	}

	/*void ChangeBoxColor(GameObject Obj, float[] samples, int boxId) {



	} */

}
