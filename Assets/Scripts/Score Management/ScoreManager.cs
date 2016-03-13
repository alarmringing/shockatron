using UnityEngine;
using UnityEngine.UI; // use UI namespace
using System.Collections;

public class ScoreManager : MonoBehaviour {

	//when eating coin generate this
	public GameObject effect;

	// create variable to increment score
	public int score;

	// variable to store the text for score
	public Text scoreText;
	string scoreKey = "currentScore";

	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt (scoreKey);
		setScoreText ();
	}

	void update (){

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

	void OnTriggerEnter( Collider other) {
		if (other.tag == "Coin") {
			score += 5;
			Destroy(other.gameObject);
			Debug.Log("ate ball, time is " + Time.time);
			Instantiate(effect, transform.position, Quaternion.identity);
		}
		if (other.tag == "building") {
			score -= 10;
		}
		setScoreText ();
		PlayerPrefs.SetInt (scoreKey,score);
		PlayerPrefs.Save();
	}


	void setScoreText(){
		scoreText.text = "Score: " + score.ToString();
	}

}