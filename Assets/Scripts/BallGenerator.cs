using UnityEngine;
using System.Collections;

public class BallGenerator : MonoBehaviour, AudioProcessor.AudioCallbacks {

	public Material colorChangeMat;
	public GameObject ballToEat;
	public GameObject player;
	public float generateDist;
	//float playerMoveSpeed = 4f;

	// Use this for initialization
	void Start () {
		
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
		generateDist = 100f;
		//AudioProcessor processor = GetComponent<AudioProcessor>();
		//processor.addAudioCallback(this);
	}
	
	// Update is called once per frame
	void Update () {

		//playerMove();
	}

	public void onBeatDetection() 
	{
		Debug.Log("Beat detected");
		colorChangeMat.color = Random.ColorHSV();
		generateBall();
	}

	void generateBall() 
	{

		Vector3 forwardDirection = player.transform.TransformDirection(new Vector3(0, 0f, 1f))*generateDist;
		Vector3 generatePos = player.transform.position + forwardDirection;
		Debug.Log("original pos is " + player.transform.position);
		Debug.Log("forward direction is " + forwardDirection.ToString());
		Debug.Log("New position si " + (generatePos).ToString());
		//transform.TransformDirection(forwardDirection);

		Instantiate(ballToEat, generatePos, Quaternion.identity);
	}

	public void onOnbeatDetected()
	{
		Debug.Log("Beat!!!");
	}

	public void onSpectrum(float[] spectrum)
	{

	}

}
