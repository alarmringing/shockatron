using UnityEngine;
using System.Collections;

public class CameraColorChange : MonoBehaviour,  AudioProcessor.AudioCallbacks {


	Camera camera;
	public Color color1 = Color.black;
	public Color color2 = Color.white;
	public float fadeTime = 0.5f;

	// Use this for initialization
	void Start () {

		camera = Camera.main;
		//camera.clearFlags = CameraClearFlags.SolidColor;

		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		//processor.addAudioCallback(this);

	}

	// Update is called once per frame
	void Update () {

	}
		
	public void onBeatDetection() 
	{
		/*
		Debug.Log("Time to change screen! Time is " + Time.time);
		camera.backgroundColor = color2; //Color.Lerp(color1, color2, 1);
		StartCoroutine (fadeScreen (0.0f, fadeTime));*/
	}

	/*
	IEnumerator flashScreen(){
		//float t = Mathf.PingPong(Time.time, duration) / duration;
		camera.backgroundColor = Color.Lerp(color1, color2, 1);
		yield return new WaitForSeconds(screenTime);
		camera.backgroundColor = Color.Lerp(color1, color2, 0);


	}*/

	IEnumerator fadeScreen(float aValue, float aTime){
		float alpha = camera.backgroundColor.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color.Lerp(color2, color1, t);
			Color newColor = Color.Lerp(color2, color1, t);;
			//Debug.Log("new alpha is " + newColor.a);
			camera.backgroundColor = newColor;
			yield return null;
		}
	}

	public void onOnbeatDetected()
	{
	}

	public void onSpectrum(float[] spectrum)
	{

	}

}

