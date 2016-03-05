using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Get_Score : MonoBehaviour {


	// variable to store the text for score
	public Text scoreText;
	public int score;
	string scoreKey = "currentScore";


	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt (scoreKey);
		Debug.Log (score);
		scoreText.text = "Score: " + score.ToString();

		PlayerPrefs.SetInt (scoreKey, 0);

	}
		
}
