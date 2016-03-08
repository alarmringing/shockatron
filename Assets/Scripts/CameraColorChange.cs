using UnityEngine;
using System.Collections;

public class CameraColorChange : MonoBehaviour,  AudioProcessor.AudioCallbacks {


	Camera camera;
	public Color color1 = Color.black;
	public Color color2 = Color.white;

	public float duration = 1.0f;
	public float screenTime = 0.05f;
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
		StartCoroutine (flashScreen ());
	}

	IEnumerator flashScreen(){
		Debug.Log("Flashing Screen");
		//float t = Mathf.PingPong(Time.time, duration) / duration;
		camera.backgroundColor = Color.Lerp(color1, color2, 1);
		yield return new WaitForSeconds(screenTime);
		camera.backgroundColor = Color.Lerp(color1, color2, 0);


	}

	public void onOnbeatDetected()
	{
	}

	public void onSpectrum(float[] spectrum)
	{

	}

}

