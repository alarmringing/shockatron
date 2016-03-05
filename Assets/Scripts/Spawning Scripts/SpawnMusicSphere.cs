using UnityEngine;
using System.Collections;

public class SpawnMusicSphere : MonoBehaviour {

	// set game objects attached to the sphere
	public GameObject spectrumBox;
	public GameObject planet;

	// set the audio samples
	int numSamples = 1024;
	AudioSource thisAudio;
	float[] samples;
	float outputNum = 1024;
	float totalSum;
	float volume = 30f;

	// set number of boxes
	float numBoxes = 100;
	GameObject[] spectrumBoxes;

	// set parameters for box scaling
	Vector3 boxScale;
	float maxStretch = 2f;

	// Use this for initialization
	void Start () {

		thisAudio = gameObject.GetComponent<AudioSource>();
		samples = new float[numSamples];
		SpawnBoxes();

	}

	// Update is called once per frame
	void Update () {

		thisAudio.GetSpectrumData(samples, 0, FFTWindow.Hamming);

		float minValue = float.MaxValue;
		for(int j=0; j<numSamples; j++)
		{
			if(samples[j] < minValue) minValue = samples[j];
		}

		if(spectrumBoxes[0]){
			for(int i=0; i<numBoxes; i++) 
			{
				SetBoxScale(spectrumBoxes[i], samples, i);
			}
		}

	}

	void SpawnBoxes()
	{

		// set default colors
		int color = 0; 
		int numberColors = 5;
		Vector4[]  colors = new Vector4[numberColors];
		colors[0] =  new Vector4 (122/255.0f, 255/255.0f,0f,1f);
		colors[1] =  new Vector4 (31/255.0f, 196/255.0f, 244/255.0f,1f);
		colors[2] =  new Vector4 (0/255.0f, 61/255.0f, 244/255.0f,1f);
		colors[3] =  new Vector4 (45/255.0f, 44/255.0f, 155/255.0f,1f);
		colors[4] =  new Vector4 (0/255.0f, 116/255.0f, 188/255.0f,1f);


		// create object_number of objects
		for (int i = 0; i < numBoxes; i++) {

			// determine rotation and position of sphere
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + spectrumBox.transform.localScale.y * 0.5f) + planet.transform.position;

			// initatiate the object
			GameObject newObject = Instantiate(spectrumBox, spawnPosition, spawnRotation) as GameObject;

			// transform the object
			newObject.transform.LookAt(planet.transform);
			newObject.transform.Rotate(-90, 0, 0);

			/* RANDOMIZE THE WIDTH OF THE OBJECT */
			// randomly scale the size of the object
			float width_scale =  Random.Range(0F, 10F);
			float height_scale = 0; //Random.Range (-1F, 1F);
			newObject.transform.localScale += new Vector3(width_scale, height_scale, width_scale);

			/* SET THE COLOR */ 
			Renderer objectRender = newObject.GetComponent<Renderer> ();
			objectRender.material.color = colors[color];

			color++;
			if (color == numberColors -1) color = 0;
		}

		spectrumBoxes = GameObject.FindGameObjectsWithTag("SpectrumCube");

	}

	void SetBoxScale(GameObject Obj, float[] samples, int boxId) {

		float avg = samples[boxId];
		float stretchValue =  Mathf.Clamp01(avg) * maxStretch;
		Vector3 previousScale = Obj.transform.localScale;

		previousScale.y = Mathf.Lerp(previousScale.y, stretchValue * 40, Time.deltaTime * 30);
		Obj.transform.localScale = previousScale;
		Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.localScale.y/2, Obj.transform.position.z);
	}
}
