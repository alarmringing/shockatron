using UnityEngine;
using System.Collections;

public class BallGenerator : MonoBehaviour, AudioProcessor.AudioCallbacks {

	public Material colorChangeMat;
	public GameObject ballToEat;
	public GameObject player;
	float generateDist = 7f;
	float playerMoveSpeed = 4f;

	// Use this for initialization
	void Start () {
		
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
		//AudioProcessor processor = GetComponent<AudioProcessor>();
		//processor.addAudioCallback(this);
	}
	
	// Update is called once per frame
	void Update () {

		playerMove();
	}

	void playerMove()
	{
		Vector3 prevPos = player.transform.position;
		prevPos.z = prevPos.z + playerMoveSpeed * Time.deltaTime;
		player.transform.position = prevPos;
	}

	public void onBeatDetection() 
	{
		Debug.Log("Beat detected");
		colorChangeMat.color = Random.ColorHSV();
		generateBall();
	}

	void generateBall() 
	{
		Vector3 generatePos = player.transform.position;
		generatePos.z += generateDist;
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
