using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Get_Score : MonoBehaviour {


	// variable to store the text for score
	public Text scoreText;
	public Text buildingText;
	public Text failText;
	public int score;
	public int energy;
	public int life;
	public int buildingsLeft;
	string scoreKey = "currentScore";
	string lifeKey = "currentLife";
	string energyKey = "currentEnergy";
	string buildingsKey = "buildingsRemaining";

	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt (scoreKey);
		life = PlayerPrefs.GetInt (lifeKey);
		energy = PlayerPrefs.GetInt (energyKey);
		buildingsLeft = PlayerPrefs.GetInt(buildingsKey);

		if (life > 0) {
			Debug.Log (score);
			scoreText.color = Color.cyan;
			scoreText.text = "Score: " + (score * life / 100.0f).ToString ();
			if (buildingsLeft == 0) {
				buildingText.color = Color.cyan;
			} else {
				buildingText.color = new Vector4 (231f / 255f, 86f / 255f, 4f / 255f, 1f); 
			}
			buildingText.text = "Buildings Remaining: " + buildingsLeft.ToString ();
		} 
		else {

			failText.text = "The Music Destroyed You!\nTry again?";
		}

		// 

	}
		
}
