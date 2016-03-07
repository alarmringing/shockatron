using UnityEngine;
using System.Collections;

public class CameraColorChange : MonoBehaviour,  AudioProcessor.AudioCallbacks {


	Camera camera;
	public Color color1 = Color.black;
	public Color color2 = Color.white;

	public float duration = 1.0f;
	public float fadeTime = 0.1f;
	public float screenTime = 0.1f;
	private float lastBeat;

	// Use this for initialization
	void Start () {

		camera = Camera.main;
		camera.clearFlags = CameraClearFlags.SolidColor;

		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
	}

	// Update is called once per frame
	void Update () {

	}
		
	public void onBeatDetection() 
	{
		flashScreen();
	}

	void flashScreen(){
		Debug.Log("Flashing Screen");
		float t = Mathf.PingPong(Time.time, duration) / duration;
		camera.backgroundColor = Color.Lerp(color1, color2, t);
		Debug.Log (t);


		// ideally face back to original color after change to white




	}

	public void onOnbeatDetected()
	{
	}

	public void onSpectrum(float[] spectrum)
	{

	}

}

