using UnityEngine;
using System.Collections;

public class BuildingAnimations : MonoBehaviour {

	public GameObject planet;

	// controls for the buildings
	GameObject[] goodBuildings;
	GameObject[] badBuildings;
	bool buildingSpawned = false;
	//origin positions of buildings for keeping them on ground
	Vector3[] GoodOriginPos; 
	Vector3[] BadOriginPos;

	// audio paramters
	AudioSource[] audioSources;
	AudioSource goodAudio;
	AudioSource badAudio;
	float[] samples;
	float[] noiseSamples;
	float stretchIntensity = 850f;
	float stretchSpeed = 10f;
	float baseHeight = 25f;
	float baseWidth = 10f;
	int numSamples = 1024;

	// Use this for initialization
	void Start () {
		audioSources = gameObject.GetComponents<AudioSource>();
		goodAudio = audioSources[0];
		badAudio = audioSources[1];
		samples = new float[numSamples];
		noiseSamples = new float[numSamples];

	}

	public void OnSpawnFinished(Vector3[] GoodBuildingsPos, Vector3[] BadBuildingsPos)
	{		
		goodBuildings = GameObject.FindGameObjectsWithTag ("GoodBuilding");
		badBuildings = GameObject.FindGameObjectsWithTag ("BadBuilding");
		GoodOriginPos = GoodBuildingsPos;
		BadOriginPos = BadBuildingsPos;

		buildingSpawned = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(goodAudio.isPlaying)
		{
			goodAudio.GetSpectrumData(samples, 0, FFTWindow.Hamming);
			badAudio.GetSpectrumData(noiseSamples, 0, FFTWindow.Hamming);

			float minValue = float.MaxValue;
			for(int j=0; j<numSamples; j++)
			{
				if(samples[j] < minValue) minValue = samples[j];
			}

			float minStretch = float.MaxValue; float maxStretch = 0;

			if(buildingSpawned){
				for(int i=0; i<goodBuildings.Length; i++) 
				{
					float thisStretch = SetGoodBoxScale(goodBuildings[i], i);
					if(thisStretch < minStretch) minStretch = thisStretch;
					if(thisStretch > maxStretch) maxStretch = thisStretch;
				}
				for(int j=0; j<badBuildings.Length; j++)
				{
					SetBadBoxScale(badBuildings[j], j, minStretch, maxStretch);
				}
			}
		}
	
	}

	//Good boxes move on beat
	float SetGoodBoxScale(GameObject Obj, int boxId) {

		int sampleId = (int)((samples.Length/(float)goodBuildings.Length) * boxId);
		float avg = samples[sampleId];
		float stretchValue =  Mathf.Sqrt(Mathf.Clamp01(avg));
		float baseStretchValue = 1/stretchValue;
		//Debug.Log("Stretchvalue for this goodbox is " + stretchValue);
		Vector3 previousScale = Obj.transform.localScale;

		previousScale.y = Mathf.Lerp(previousScale.y, baseHeight + stretchValue*stretchIntensity, Time.deltaTime * stretchSpeed);
		//previousScale.x = Mathf.Lerp(previousScale.x, baseWidth + baseStretchValue/100, Time.deltaTime * stretchSpeed);
		//previousScale.z = Mathf.Lerp(previousScale.z, baseWidth + baseStretchValue/100, Time.deltaTime * stretchSpeed);
		Obj.transform.localScale = previousScale;

		Vector3 sphereDirection =  (GoodOriginPos[boxId] - planet.transform.position).normalized;
		Obj.transform.position = GoodOriginPos[boxId] + sphereDirection * Obj.transform.localScale.y/2;

		return stretchValue;
	}

	//Bad boxes move randomly and emit noise
	void SetBadBoxScale(GameObject Obj, int boxId, float minStretch, float maxStretch)
	{
		//float stretchValue = minStretch + (Random.value * (maxStretch-minStretch));

		int sampleId = (int)((noiseSamples.Length/(float)badBuildings.Length) * boxId);
		float avg = noiseSamples[sampleId];
		float stretchValue =  Mathf.Sqrt(Mathf.Clamp01(avg));
		//Debug.Log("Stretchvalue for this badbox is " + stretchValue);
		Vector3 previousScale = Obj.transform.localScale;

		previousScale.y = baseHeight + stretchValue*stretchIntensity*2;
		Obj.transform.localScale = previousScale;

		Vector3 sphereDirection =  (BadOriginPos[boxId] - planet.transform.position).normalized;
		Obj.transform.position = BadOriginPos[boxId] + sphereDirection * Obj.transform.localScale.y/2;
	}

	/*void ChangeBoxColor(GameObject Obj, float[] samples, int boxId) {



	} */

}
