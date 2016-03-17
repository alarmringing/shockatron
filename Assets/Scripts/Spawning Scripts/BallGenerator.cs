using UnityEngine;
using System.Collections;

public class BallGenerator : MonoBehaviour, AudioProcessor.AudioCallbacks {

	public Material colorChangeMat;
	public GameObject ballToEat;
	public GameObject player;
	public float generateDist = 100f;
	float leftRightSpan = 0.2f;
	Vector3 lastBallPos;
	//float playerMoveSpeed = 4f;
//	private int ballCount; 

	// Use this for initialization
	void Start () {
		
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
		lastBallPos = player.transform.position;
		generateDist = 100f;
		//AudioProcessor processor = GetComponent<AudioProcessor>();
		//processor.addAudioCallback(this);
//		ballCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//playerMove();
	}

	public void onBeatDetection() 
	{
		//colorChangeMat.color = Random.ColorHSV();

		generateBall();
	}

	void generateBall() 
	{
		Vector3 generateDirection = new Vector3();
		Vector3 globalDirection = new Vector3();
		Vector3 generatePos = new Vector3();

		bool collideWithBuilding = true; //to start on while loop
		while(collideWithBuilding) 
		{
			float randomLeftRight = Random.Range(-leftRightSpan/2, leftRightSpan/2);
			generateDirection = new Vector3(randomLeftRight, -player.transform.localScale.y*0.18f, 1f).normalized;
			globalDirection = player.transform.TransformDirection(generateDirection)*generateDist;
			generatePos = player.transform.position + globalDirection;
			collideWithBuilding = false; //for now, later will check if ball gets stuck inside building
		}
		//Debug.Log("original pos is " + player.transform.position);
		//Debug.Log("forward direction is " + forwardDirection.ToString());
		//Debug.Log("New position si " + (generatePos).ToString());
		//transform.TransformDirection(forwardDirection);


		Instantiate(ballToEat, generatePos, Quaternion.identity);


		/* SET THE COLOR */ // makes each object unique to change the color
//		newBall.name = "ball_"+ballCount.ToString();
//		ballCount++;
//		Renderer objectRender = newBall.GetComponent<Renderer> ();
//		objectRender.material.color = Random.ColorHSV ();
//		Debug.Log("---CHANGED COLOR---");


	}

	public void onOnbeatDetected()
	{
		Debug.Log("Beat!!!");
	}

	public void onSpectrum(float[] spectrum)
	{

	}

}
