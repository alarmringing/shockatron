using UnityEngine;
using System.Collections;

public class StickStretcher : MonoBehaviour {

	int numSamples = 1024;
	AudioSource thisAudio;
	/*
	public GameObject Cube1;
	public GameObject Cube2;
	public GameObject Cube3;*/
	//public GameObject SurpriseParticle;

	public GameObject spectrumBox;

	float[] samples;
	float outputNum = 1024;
	float numBoxes = 50;
	GameObject[] spectrumBoxes;
	Vector3 boxScale;
	float maxStretch = 2f;
	float radius = 10f;
	float totalSum;
	float volume = 30f;

	// Use this for initialization
	void Start () {

		thisAudio = gameObject.GetComponent<AudioSource>();
		samples = new float[numSamples];
		SpawnBoxes();

		//boxScale = Cube1.transform.localScale;

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
		for (int i = 0;i < numBoxes; i++)
		{
			float angle = i * Mathf.PI * 2 / numBoxes;
			Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
			GameObject thisCube = (GameObject)(Instantiate(spectrumBox, pos, Quaternion.identity));
			//spectrumBoxes[i] = thisCube;
			//Debug.Log("This box assigned as " + spectrumBoxes[i].ToString());
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

	/*
	void SpawnParticle()
	{
		Random.Range(-3,3);
		GameObject newParticle = ((GameObject)(Instantiate(SurpriseParticle, new Vector3(0,4,0), new Quaternion())));
	}*/
}

/*
public class Visualize : MonoBehaviour {

	public GameObject m_LevelLeft;
	public GameObject m_LevelRight;
	private bool m_IsOk = false;
	private int m_NumSamples = 1024;
	private float[] m_SamplesL, m_SamplesR;
	private int i;
	private float maxL, maxR, sample, sumL, sumR, rms, dB;
	private Vector3 scaleL, scaleR;
	// Because rms values are usually very low
	private float volume = 30.0f;
	private Color color;
	// Use this for initialization
	void Start () {
		// Just proper validation
		if (m_LevelLeft != null && m_LevelRight != null) {
			m_SamplesL = new float[m_NumSamples];
			m_SamplesR = new float[m_NumSamples];
			scaleL = m_LevelLeft.transform.localScale;
			scaleR = m_LevelRight.transform.localScale;
			m_IsOk = true;
			audio.Play();
		}
		else
			Debug.Log("Missing objects linkage");
	}

	// Update is called once per frame
	void Update () {
		// Continuing proper validation
		if (m_IsOk) {
			audio.GetOutputData(m_SamplesL, 0);
			audio.GetOutputData(m_SamplesR, 1);
			maxL = maxR = 0.0f;
			sumL = 0.0f;
			sumR = 0.0f;
			for (i = 0; i < m_NumSamples; i++) {
				sumL = m_SamplesL[i] * m_SamplesL[i];
				sumR = m_SamplesR[i] * m_SamplesR[i];
			}
			rms = Mathf.Sqrt(sumL/m_NumSamples);
			scaleL.y = Mathf.Clamp01(rms*volume);
			rms = Mathf.Sqrt(sumR/m_NumSamples);
			scaleR.y = Mathf.Clamp01(rms*volume);
			m_LevelLeft.transform.localScale = scaleL;
			m_LevelRight.transform.localScale = scaleR;
			color = GetVolumeColor(scaleL.y);
			m_LevelLeft.GetComponentInChildren<Renderer>().material.color = color;
			color = GetVolumeColor(scaleR.y);
			m_LevelRight.GetComponentInChildren<Renderer>().material.color = color;
		}
	}

	Color GetVolumeColor (float volume) {
		if (volume > 0.7f)
			return Color.red;
		if (volume > 0.5f)
			return Color.yellow;
		return Color.green;
	}


}
*/