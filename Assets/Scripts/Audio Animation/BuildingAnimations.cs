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
	int numSamples = 1024;
	float maxStretch = 200f;

	// Use this for initialization
	void Start () {
		thisAudio = gameObject.GetComponent<AudioSource>();
		samples = new float[numSamples];
	}

	public void OnSpawnFinished()
	{
		buildingSpawned = true;
		buildings = GameObject.FindGameObjectsWithTag ("building");
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
			for(int i=0; i<buildings.Length; i++) 
			{
				SetBoxScale(buildings[i], samples, i);
			}
		}
	
	}

	void SetBoxScale(GameObject Obj, float[] samples, int boxId) {

		float avg = samples[boxId];
		float stretchValue =  Mathf.Clamp01(avg) * maxStretch;
		Vector3 previousScale = Obj.transform.localScale;

		previousScale.y = Mathf.Lerp(previousScale.y, stretchValue * 40, Time.deltaTime * 30);
		Obj.transform.localScale = previousScale;

		// change the box collider to the same scale
		//BoxCollider buildingCollider = Obj.GetComponent<BoxCollider>();
		//Vector3 renderSize = Obj.GetComponent<Renderer>().bounds.size;
		//buildingCollider.size = new Vector3(1,1,1);
		//buildingCollider.size = renderSize;
		//buildingCollider.center = new Vector3 ((renderSize.x / 2),(renderSize.y / 2), (renderSize.z / 2));
		//buildingCollider.size = previousScale;


		//Vector3 sphereDirection =  (planet.transform.position - transform.position).normalized;

		//Obj.transform.position = Obj.transform.localPosition - sphereDirection * Obj.transform.localScale.y / 2;

	}

	/*void ChangeBoxColor(GameObject Obj, float[] samples, int boxId) {



	} */

}
