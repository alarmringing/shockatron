using UnityEngine;
using System.Collections;

public class BuildingAnimations : MonoBehaviour {

	public GameObject planet;

	bool buildingSpawned = false;
	GameObject[] buildings;

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
		Debug.Log ("Spawn is finished!");
		buildings = GameObject.FindGameObjectsWithTag ("building");
		Debug.Log ("# of builngs is " + buildings.Length);
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


		//Vector3 sphereDirection =  (planet.transform.position - transform.position).normalized;

		//Obj.transform.position = Obj.transform.localPosition - sphereDirection * Obj.transform.localScale.y / 2;

	}

}
