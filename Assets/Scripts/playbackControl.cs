using UnityEngine;
using System.Collections;

public class playbackControl : MonoBehaviour {

	public GameObject player;
	public GameObject beatGen_early; 
	public float genDist;
	public float delay;
	float moveSpeed;


	// Use this for initialization
	void Start () {

		//delay playback
		AudioSource thisAudio = GetComponent<AudioSource>();


		moveSpeed = player.GetComponent<FauxGravity_controller>().moveSpeed; //gets movespeed of player
		genDist = beatGen_early.GetComponent<BallGenerator>().generateDist;
		delay = genDist / moveSpeed;

		thisAudio.PlayDelayed(delay);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
