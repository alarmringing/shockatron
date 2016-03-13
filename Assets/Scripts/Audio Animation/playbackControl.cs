using UnityEngine;
using System.Collections;

public class playbackControl : MonoBehaviour {

	public GameObject player;
	public GameObject beatGen_early; 
	AudioSource thisAudio;
	public float genDist;
	public float delay;
	float moveSpeed;
	float startTime;


	// Use this for initialization
	void Start () {

		//delay playback
		thisAudio = GetComponent<AudioSource>();
		thisAudio.Stop();
		startTime = Time.time;

		moveSpeed = player.GetComponent<FauxGravity_controller>().moveSpeed; //gets movespeed of player
		genDist = beatGen_early.GetComponent<BallGenerator>().generateDist;
		delay = genDist / moveSpeed;
		Debug.Log("movespeed is " + moveSpeed + "genDist is " + genDist + "Delay is " + delay);
	
	}

	// Update is called once per frame
	void Update () {
	
		if(!thisAudio.isPlaying && (Time.time-startTime) > delay) 
		{
			thisAudio.Play();
		}
	}
		
}
