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

	// whether attacking mode or not
	bool isAttackMode;

	// create variable to increment score
	public int score;
	int life;
	float energy; //will be a bar

	// variable to store the text for score
	public Text scoreText;
	string scoreKey = "currentScore";
	string lifeKey = "currentLife";
	string energyKey = "currentEnergy";
	string buildingsKey = "buildingsRemaining";

	int goalNum = 20; //number of bad buildings, for now

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt( scoreKey, 0); //set score to 0 every time for now
		PlayerPrefs.SetInt(lifeKey, 100);
		PlayerPrefs.SetInt(energyKey, 50);
		PlayerPrefs.SetInt(buildingsKey, goalNum);

		score = 0;
		//PlayerPrefs.GetInt (scoreKey); 
		life = 100;
		energy = 50f;
		setScoreText ();
	}

	void Update (){

		//activate attack mode when space is on
		if(Input.GetKey("space")) 
		{
			Debug.Log("Attack moode!");
			isAttackMode = true;
			energy -= 5 * Time.deltaTime; //expend energy every time attack mode is activated
			Charge_normal.SetActive(false);
			Charge_attack.SetActive(true);
		}
		else
		{
			isAttackMode =false;
			Charge_normal.SetActive(true);
			Charge_attack.SetActive(false);
		}

		// If deplete life, end the game
		if (life <= 0) {
			SceneManager.LoadScene ("GameEndScene");
		}

	}

	void OnDisable(){
		// make list of the 5 highest scores
		int[] highScores = new int[5];
		int testScore = score;

		for (int i = 0; i < highScores.Length; i++){

			//Get the highScore from 1 - 5
			string highScoreKey = "HighScore"+(i+1).ToString();
			int highScore = PlayerPrefs.GetInt(highScoreKey,0);

			//if score is greater, store previous highScore
			//Set new highScore
			//set score to previous highScore, and try again
			//Once score is greater, it will always be for the
			//remaining list, so the top 5 will always be 
			//updated
			if(testScore > highScore){
				int temp = highScore;
				PlayerPrefs.SetInt (highScoreKey, score);
				testScore = temp;
				PlayerPrefs.Save();
			}
		}


	}

	void OnCollisionEnter( Collision other) {

		Debug.Log("Currently in attack mode? " + isAttackMode);

		if (other.gameObject.tag == "Coin") {
			score += 5;
			energy += 2f;
			Destroy(other.gameObject);
			Debug.Log("ate ball, time is " + Time.time);
			Instantiate(CoinEateffect, transform.position, Quaternion.identity);
		}
		if (other.gameObject.tag == "GoodBuilding" || other.gameObject.tag == "BadBuilding") {

			if(!isAttackMode) //if not in attacking mode, just a crash 
			{
				score -= 10;
				life -= 10;
				Debug.Log("Hitting building now");
				BuildingCollideEffect.SetActive(true);
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
					Instantiate(BuildingDestroyEffect, transform.position, Quaternion.identity);
				}
				else //destroyed bad building, good!
				{
					goalNum -= 1;
					PlayerPrefs.SetInt(buildingsKey, goalNum);
				}					
			}
				
		}
		setScoreText ();
		PlayerPrefs.SetInt (scoreKey,score);
		PlayerPrefs.Save();
	}
		

	void setScoreText(){
		scoreText.text = "Life: " + life.ToString() + "\nEnergy: " + energy.ToString() + "\nGoals: " + goalNum;
	}

}