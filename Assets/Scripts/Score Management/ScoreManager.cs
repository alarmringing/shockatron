using UnityEngine;
using UnityEngine.UI; // use UI namespace
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	//when eating coin generate this
	public GameObject CoinEateffect;
	public GameObject BuildingCollideEffect;
	public GameObject BuildingDestroyEffect;
	public GameObject Charge_normal;
	public GameObject Charge_attack;

	AudioSource buildingExplode; 
	AudioSource buildingHit;
	AudioSource coinCollect;

	// whether attacking mode or not
	bool isAttackMode;

	// create variable to increment score
	public int score;
	int life;
	float energy; //will be a bar

	// text to flash the score changes
	public Text scoreIndicator;
	public float displayDelay = 0.3f;

	// variable to store the text for score
	public Text scoreText;
	string scoreKey = "currentScore";
	string lifeKey = "currentLife";
	string energyKey = "currentEnergy";
	string buildingsKey = "buildingsRemaining";

	int goalNum = 20; //number of bad buildings, for now

	// Use this for initialization
	void Start () {
		// set initial conditions 
		PlayerPrefs.SetInt( scoreKey, 0); //set score to 0 every time for now
		PlayerPrefs.SetInt(lifeKey, 100);
		PlayerPrefs.SetInt(energyKey, 50);
		PlayerPrefs.SetInt(buildingsKey, goalNum);

		score = 0;
		//PlayerPrefs.GetInt (scoreKey); 
		life = 100;
		energy = 50;
		setScoreText ();

		scoreIndicator.text = "";

		// load audioclip
		AudioSource[] soundEffects = GetComponents<AudioSource>();
		buildingExplode = soundEffects [1]; 
		buildingHit = soundEffects [2];
		coinCollect = soundEffects [3];

	}

	void Update (){

		//activate attack mode when space is on
		if(Input.GetKey("space")) 
		{
			Debug.Log("Attack moode!");
			isAttackMode = true;
			energy -= 7 * Time.deltaTime; //expend energy every time attack mode is activated
			Charge_normal.SetActive(false);
			Charge_attack.SetActive(true);
		}
		else
		{
			isAttackMode =false;
			energy -= 1 * Time.deltaTime; //user spends energy just traveling too
			Charge_normal.SetActive(true);
			Charge_attack.SetActive(false);
		}

		// If deplete life, end the game
		if (life <= 0) {
			PlayerPrefs.SetInt (lifeKey,life);
			SceneManager.LoadScene ("GameEndScene");
		}

	}


	void OnCollisionEnter( Collision other) {

		Debug.Log("Currently in attack mode? " + isAttackMode);

		if (other.gameObject.tag == "Coin") {
			score += 5;
			energy += 2f;
			Destroy(other.gameObject);
			Debug.Log("ate ball, time is " + Time.time);
			coinCollect.Play ();
			StartCoroutine(displayScore("+5!")); // display change in score
			Instantiate(CoinEateffect, transform.position, Quaternion.identity);

		}
		if (other.gameObject.tag == "GoodBuilding" || other.gameObject.tag == "BadBuilding") {


			if(!isAttackMode) //if not in attacking mode, just a crash 
			{
				score -= 10;
				life -= 10;
				Debug.Log("Hitting building now");
				buildingHit.Play ();
				StartCoroutine(displayScore("Ouch!\n-10!")); // display change in score
				//BuildingCollideEffect.SetActive(true);
				//Instantiate(BuildingCollideEffect, transform.position, Quaternion.identity);

			}
			else //is attack mode
			{
				//Destroy(other.gameObject); //destroy that other building
				other.gameObject.SetActive(false);



				if(other.gameObject.tag == "GoodBuilding")  //oops
				{
					Debug.Log("Hit a good building");
					life -= 30;
					buildingHit.Play ();
					StartCoroutine(displayScore("You Killed the Beat!\n-30!")); // display change in score
					//Instantiate(BuildingDestroyEffect, transform.position, Quaternion.identity);
				}
				else //destroyed bad building, good!
				{
					goalNum -= 1;
					buildingExplode.Play();
					StartCoroutine(displayScore("Keeping the Beat!")); // display change in score
					StartCoroutine(increaseVolume()); // increase volume
					PlayerPrefs.SetInt(buildingsKey, goalNum);
				}					
			}
				
		}
		setScoreText ();
		PlayerPrefs.SetInt (scoreKey,score);
		PlayerPrefs.SetInt (lifeKey,life);
		PlayerPrefs.SetFloat (energyKey,energy);
		PlayerPrefs.Save();
	}

	// display text for certain amount of time (in center of screen)
	IEnumerator displayScore(string display){

		scoreIndicator.text = display;
		yield return new WaitForSeconds(displayDelay);
		scoreIndicator.text = "";
	}

	IEnumerator increaseVolume(){

		AudioListener.volume *= 5;
		yield return new WaitForSeconds(.5f);
		AudioListener.volume /= 5;
	}

	void setScoreText(){
		scoreText.text = "Life: " + life.ToString() + "\nEnergy: " + energy.ToString("F1") + "\nOffbeat Buildings: " + goalNum;
	}
//	void OnDisable(){
//		// make list of the 5 highest scores
//		int[] highScores = new int[5];
//		int testScore = score;
//
//		for (int i = 0; i < highScores.Length; i++){
//
//			//Get the highScore from 1 - 5
//			string highScoreKey = "HighScore"+(i+1).ToString();
//			int highScore = PlayerPrefs.GetInt(highScoreKey,0);
//
//			//if score is greater, store previous highScore
//			//Set new highScore
//			//set score to previous highScore, and try again
//			//Once score is greater, it will always be for the
//			//remaining list, so the top 5 will always be 
//			//updated
//			if(testScore > highScore){
//				int temp = highScore;
//				PlayerPrefs.SetInt (highScoreKey, score);
//				testScore = temp;
//				PlayerPrefs.Save();
//			}
//		}
//
//
//	}




}